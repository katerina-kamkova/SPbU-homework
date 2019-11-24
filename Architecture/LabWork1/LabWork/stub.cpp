// Функции вычисления синдромов и восстановления для отладки шаблона
#pragma once
#include <stdlib.h>
#include "stdafx.h"
#ifdef RDX_STUB
#define __RDX_GF_VECTOR__
#define GF_2_8_MODULUS 0x71

// Class GF8 provides the functionality of GF(2^8)
class GF8
{
private:
	char v;
	inline static char mulx_8(char a)
	{
		char mask = a >= 0 ? 0 : GF_2_8_MODULUS;
		return (a << 1) ^ mask;
	}
public:
	// Constructors
	inline GF8(void){};
	inline GF8(char init) :v(init){};
	inline operator char() { return v; }
	// Arithmetic
	inline GF8 operator+(GF8 b) { return GF8(v^b.v); }
	inline GF8 operator-(GF8 b) { return *this + b; }
	inline GF8 operator*(GF8 b)
	{
		char mask;
		char production = 0;
		for (unsigned int i = 0; i < 8; i++)
		{
			production = mulx_8(production);
			mask = b.v >= 0 ? 0 : v;
			production ^= mask;
			b.v <<= 1;
		}
		return GF8(production);
	}
	inline GF8 operator/(GF8 b) { return *this * inverse(b); }
	inline GF8 operator^(unsigned int n) // Power
	{
		// Donald E. Knuth. Sec. 4.6.3 (Vol 2)
		GF8 Y(1);
		GF8 Z(v);

		while (n > 0)
		{
			if (0 != (n & 1))
				Y *= Z;
			Z *= Z;
			n >>= 1;
		}
		return(Y);
	} // Power

	inline GF8& operator+=(GF8 b) { return *this = *this + b; }
	inline GF8& operator-=(GF8 b) { return *this = *this - b; }
	inline GF8& operator*=(GF8 b) { return *this = *this * b; }
	inline GF8& operator/=(GF8 b) { return *this = *this / b; }

	inline GF8& mulx() { v = mulx_8(v); return *this; }
	inline static GF8 inverse(GF8 x)
	{
		GF8 Y;
		GF8 Z;
		Z = x * x;
		Y = Z;
		for (int i = 0; i < 6; i++)
		{
			Z *= Z;
			Y *= Z;
		}
		return(Y);
	}

	inline bool operator==(GF8 b) { return v == b.v; }
	inline bool operator!=(GF8 b) { return !(*this == b); }
#ifdef _IOSTREAM_
	friend ostream& operator<<(ostream& os, GF8 a)
	{
		os << hex << setfill('0') << setw(2) << ((unsigned int)a.v & 0xFF) << " ";
		return os;
	}
#endif
};

// Class GF8_Vector provides the functionality of vector of 32 elements over GF(2^8).
class GF8_Vector
{
private:
	__m256i v;
	inline static void mulx_256(__m256i& a)
	{
		__m256i mask = _mm256_cmpgt_epi8(_mm256_setzero_si256(), a);
		mask = _mm256_and_si256(mask, _mm256_set1_epi8(GF_2_8_MODULUS));
		a = _mm256_slli_epi16(a, 1);
		a = _mm256_and_si256(a, _mm256_set1_epi8(0xFE));
		a = _mm256_xor_si256(a, mask);
	}
#ifdef __RDX_PERFORMANCE_MEASUREMENT___
	static __int64 mulx_timer;
#endif

public:
	// Constructors
	inline GF8_Vector(){}
	inline GF8_Vector(void* source) { v = _mm256_loadu_si256((__m256i*)source); }
	inline GF8_Vector(__m256i source) : v(source) {}
	inline GF8_Vector(GF8_Vector& source) : v(source.v) {}
	inline GF8_Vector(char source) { v = _mm256_set1_epi8(source); }
	
	inline void* store(void* dst) {
		_mm256_storeu_si256((__m256i*)dst, v);
		return dst;
	}

	// Arithmetic 
	inline GF8_Vector& setzero(void) { v = _mm256_setzero_si256(); return *this; }
	inline GF8_Vector operator+(GF8_Vector b) { return GF8_Vector(_mm256_xor_si256(v, b.v)); }
	inline GF8_Vector operator-(GF8_Vector b) { return *this + b; }
	inline GF8_Vector operator*(GF8& b)
	{
		__m256i mask, production, factor;
		production = _mm256_setzero_si256();
		factor = _mm256_set1_epi8(b);
		for (unsigned int i = 0; i < 8; i++)
		{
			mulx_256(production);
			mask = _mm256_cmpgt_epi8(_mm256_setzero_si256(), factor);
			mask = _mm256_and_si256(mask, v);
			production = _mm256_xor_si256(production, mask);
			factor = _mm256_slli_epi16(factor, 1);
		}
		return GF8_Vector(production);
	}
	inline friend GF8_Vector operator*(GF8&a, GF8_Vector& b) { return b*a; }

	inline GF8_Vector& operator+=(GF8_Vector& b) { return *this = *this + b; }
	inline GF8_Vector& operator-=(GF8_Vector& b) { return *this = *this - b; }
	inline GF8_Vector& operator*=(GF8& b) { return *this = *this * b; }

	inline GF8_Vector& mulx()
	{
#ifdef __RDX_PERFORMANCE_MEASUREMENT___
		__int64 Start = __rdtsc();
#endif
		mulx_256(v);
#ifdef __RDX_PERFORMANCE_MEASUREMENT___
		__int64 Stop = __rdtsc();
		mulx_timer += (Stop - Start);
#endif

		return *this;
	}

	inline bool operator==(GF8_Vector& b)
	{
		for (int i = 0; i < 4; i++)
		if (v.m256i_i64[i] != b.v.m256i_i64[i])
			return false;
		return true;
	}
	inline bool operator!=(GF8_Vector& b) { return !(*this == b); }
#ifdef _IOSTREAM_
	friend ostream& operator<<(ostream& os, GF8_Vector& a)
	{
		for (int i = 0; i < 32; i++)
			os << hex << setfill('0') << setw(2) << (((unsigned int)a.v.m256i_i8[i]) & 0xFF) << " ";
		return os;
	}
#endif
#ifdef __RDX_PERFORMANCE_MEASUREMENT___
	inline static void ClearTimer() { mulx_timer = 0; }
	inline static __int64 GetTimer() { return mulx_timer; }
#endif
};

GF8 Table1[255]; // 1/(x^i)
GF8 Table2[255]; // 1/(1-x^i)

void PrepareTables_stub(unsigned int N)
{
	GF8 Equity(1);
	GF8 x_i(1);
	Table1[0] = Table2[0] = Equity;

	for (unsigned int i = 1; i < N; i++)
	{
		x_i.mulx();
		Table1[i] = GF8::inverse(x_i);
		Table2[i] = GF8::inverse(Equity - x_i);
	}
}


inline void RAID6_calculate(GF8_Vector& P, GF8_Vector& Q, GF8_Vector* D, unsigned int N)
{
	P.setzero();
	Q.setzero();
	for (unsigned int i = 0; i < N; i++)
	{
		P += D[i];
		Q.mulx();
		Q += D[N - 1 - i];
	}
}

inline void RAID6_recovery(GF8_Vector& P, GF8_Vector& Q, GF8_Vector* D, unsigned int N, unsigned int alpha, unsigned int beta)
{
	GF8_Vector _P, _Q;

	D[alpha].setzero();
	D[beta].setzero();
	RAID6_calculate(_P, _Q, D, N);
	_P = P - _P;
	_Q = Q - _Q;
	D[beta] = (_P + _Q*Table1[alpha])*Table2[beta - alpha];
	D[alpha] = _P + D[beta];
}

extern "C" void CalculateSyndromes_stub(void *D, unsigned int N)
{
	GF8_Vector* vD = (GF8_Vector*)D;
	GF8_Vector vP, vQ;
	RAID6_calculate(vP, vQ, vD, N);
	vP.store((void*)((__m256i*)D + N));
	vQ.store((void*)((__m256i*)D + N + 1));
}

extern "C" void Recover_stub(void *D, unsigned int N, unsigned int a, unsigned int b)
{

	GF8_Vector* vD = (GF8_Vector*)D;
	GF8_Vector vP, vQ;
	vP = GF8_Vector((__m256i*)D + N);
	vQ = GF8_Vector((__m256i*)D + N + 1);
//	PrepareTables_stub(N);
	if (a > b) {
		unsigned int c = a;
		a = b;
		b = c;
	}
	RAID6_recovery(vP, vQ, vD, N, a, b);
}
#endif // RDX_STUB