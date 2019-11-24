
// ChildView.cpp : implementation of the CChildView class
//

#include "stdafx.h"
#include "RAID6.h"
#include "ChildView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#ifdef RDX_STUB
void PrepareTables_stub(unsigned int N);
#endif

// CChildView

CChildView::CChildView()
: m_Stripe(NULL)
, m_Stripe_Recovered(NULL)
, m_nDisk(0)
, m_bSyndromesCalculated(FALSE)
, m_bDiskDestroyed(FALSE)
, m_bRecovered(FALSE)
, m_bEasyStorage(FALSE)
, m_bTestDone(FALSE)
, m_bTestFailed(FALSE)
{
#ifdef RDX_STUB
	PrepareTables_stub(MAX_DISK);
#endif
}

CChildView::~CChildView()
{
}


BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_PAINT()
	ON_COMMAND(ID_EASYSTORAGE_CREATE, &CChildView::OnEasystorageCreate)
	ON_UPDATE_COMMAND_UI(ID_EASYSTORAGE_CALC, &CChildView::OnUpdateEasystorageCalc)
	ON_COMMAND(ID_EASYSTORAGE_CALC, &CChildView::OnEasystorageCalc)
	ON_UPDATE_COMMAND_UI(ID_EASYSTORAGE_DESTROYTWODRIVES, &CChildView::OnUpdateEasystorageDestroy)
	ON_COMMAND(ID_EASYSTORAGE_DESTROYTWODRIVES, &CChildView::OnEasystorageDestroy)
	ON_UPDATE_COMMAND_UI(ID_EASYSTORAGE_RECOVERTWODRIVES, &CChildView::OnUpdateEasystorageRecover)
	ON_COMMAND(ID_EASYSTORAGE_RECOVERTWODRIVES, &CChildView::OnEasystorageRecovertwodrives)
	ON_COMMAND(ID_RANDOMSTORAGE_CREATEANDCALCULATESYNDROMES, &CChildView::OnRandomstorageCreate)
	ON_UPDATE_COMMAND_UI(ID_RANDOMSTORAGE_DESTROYANDRECOVER, &CChildView::OnUpdateRandomstorageRecover)
	ON_COMMAND(ID_RANDOMSTORAGE_DESTROYANDRECOVER, &CChildView::OnRandomstorageRecover)
	ON_COMMAND(ID_TEST, &CChildView::OnTest)
	ON_MESSAGE(WM_TEST, &CChildView::OnTestMessage)
END_MESSAGE_MAP()



// CChildView message handlers

BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs) 
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle |= WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW|CS_DBLCLKS, 
		::LoadCursor(NULL, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW+1), NULL);

	return TRUE;
}

void CChildView::OnPaint() 
{
	CPaintDC dc(this); // device context for painting
	
	// TODO: Add your message handler code here
	TCHAR txt[128];
	TEXTMETRIC mtr;

	if (m_bTestDone) {
		CPen pen_calc(PS_SOLID, 1, RGB(0, 0, 0xFF));
		CPen pen_rec(PS_SOLID, 1, RGB(0xFF, 0, 0x10));
		CPen pen_hor(PS_SOLID, 1, RGB(0xC0, 0xC0, 0xC0));
		GetTextMetrics(dc, &mtr);
		RECT Area(dc.m_ps.rcPaint);
		int bdr_width = (Area.right - Area.left) / 20;
		Area.left += bdr_width;
		Area.right -= bdr_width;
		int FontHight = mtr.tmHeight + mtr.tmExternalLeading;
		int FontWidth = mtr.tmAveCharWidth;
		int bdr_hight = (Area.bottom - Area.top) / 10 + FontHight;
		Area.top += bdr_hight;
		Area.bottom -= bdr_hight;

		int width = Area.right - Area.left;
		int hight = Area.bottom - Area.top;

		dc.MoveTo(Area.left, Area.top);
		dc.LineTo(Area.left, Area.bottom);
		// Горизонтальная ось координат
		dc.MoveTo(Area.left, Area.bottom);
		dc.LineTo(Area.right, Area.bottom);

		// Нахождение максимальногозначения времени
		__int64 maxtime = 0;
		for (int i = MIN_DISK; i <= MAX_DISK; i++) {
			maxtime = max(maxtime, calctime[i]);
			maxtime = max(maxtime, rectime[i]);
		}

		// Рисование горизонтальных линий
		int decdigits; // количество значащих десятичных цифр
		int w = maxtime;

		double amplitude = double(hight) / double(maxtime);
		double hstep = double(width) / double(MAX_DISK - MIN_DISK);

		for (decdigits = 0; w > 0; decdigits++)
			w /= 10;
		int vlstep = 1;
		for (int i = 0; i < decdigits - 1; i++)
			vlstep *= 10;
		if (maxtime / vlstep < 5)
			vlstep /= 2;

		CPen* pPen = dc.SelectObject(&pen_hor);

		for (int y = vlstep; y < maxtime; y += vlstep) {
			dc.MoveTo(Area.left + 1, Area.bottom - y*amplitude);
			dc.LineTo(Area.right, Area.bottom - y*amplitude);
			_stprintf(txt, _T("%d"), y);
			dc.TextOut(Area.left + 1, Area.bottom - y*amplitude - FontHight - 1, txt, _tcslen(txt));
		}

		// Рисование отсчетов

		dc.SelectObject(&pen_calc);
		int ex, ey;
		for (int i = MIN_DISK; i <= MAX_DISK; i++) {
			int x = Area.left + int(round(double(i - MIN_DISK) * hstep));
			int y = Area.bottom - int(round(calctime[i] * amplitude));
			dc.MoveTo(x, Area.bottom);
			dc.LineTo(x, y);
			dc.Ellipse(x - 3, y - 3, x + 3, y + 3);
		}
		ex = Area.left + 3;
		ey = Area.bottom + (5 * FontHight) / 2 + 3;
		dc.Ellipse(ex - 3, ey - 3, ex + 3, ey + 3);
		ex += 10;
		ey -= FontHight / 2;
		CString str = _T("Calculation time");
		dc.TextOut(ex, ey, str);
		CSize cs = dc.GetTextExtent(str);
		ex += cs.cx + 6 + FontWidth;
		ey += FontHight / 2;

		dc.SelectObject(&pen_rec);
		for (int i = MIN_DISK; i <= MAX_DISK; i++) {
			int x = Area.left + int(round(double(i - MIN_DISK) * hstep));
			int y = Area.bottom - int(round(rectime[i] * amplitude));
			dc.MoveTo(x, Area.bottom - int(round(calctime[i] * amplitude)) - 3);
			dc.LineTo(x, y);
			dc.Ellipse(x - 3, y - 3, x + 3, y + 3);
		}
		dc.Ellipse(ex - 3, ey - 3, ex + 3, ey + 3);
		ex += 10;
		ey -= FontHight / 2;
		str = _T("Recovery time");
		dc.TextOut(ex, ey, str);

		dc.SelectObject(pPen);

		dc.TextOut(Area.left, Area.top - FontHight*2, _T("Time"), 4);
		dc.TextOut(Area.right - FontWidth * 2, Area.bottom + 1, _T("Data"), 4);
		dc.TextOut(Area.right - FontWidth * 2, Area.bottom + 1 + FontHight, _T("Drives"), 6);
		for (int i = MIN_DISK; i < MAX_DISK; i++) {
			int x = Area.left + int(round(double(i - MIN_DISK) * hstep));
			_stprintf(txt, _T("%d"), i);
			if (i < 10) // Один символ
				dc.TextOut(x - FontWidth / 2, Area.bottom + 1, txt, 1);
			else         // Два символа
				dc.TextOut(x - FontWidth, Area.bottom + 1, txt, 2);
		}

		return;
	}

	if (0 == m_nDisk)
		return;

	COLORREF color_Disk = RGB(0, 0, 0x80);
	COLORREF color_Lost = RGB(0, 0, 0x80);
	COLORREF color_Syndrome = RGB(0, 0, 0x80);
	COLORREF color_Bk = RGB(0xFF, 0xFF, 0xFF);
	COLORREF color_BkLost = RGB(0xFF, 0x80, 0x80);
	COLORREF color_OK = RGB(0, 0xC0, 0);
	COLORREF color_Err = RGB(0xC0, 0, 0);

	int txt_y;
	dc.GetTextMetrics(&mtr);
	int v_step = mtr.tmHeight + mtr.tmExternalLeading;
	int hpos_name = mtr.tmMaxCharWidth;
	int hpos_D = mtr.tmMaxCharWidth * 4;
	int max_D_width = -1;
	int hpos_R;

	txt_y = v_step;
	for (int i = 0; i < m_nDisk; i++) {
		if (m_bDiskDestroyed && ((i == m_Destroyed[0]) || (i == m_Destroyed[1]))) {
			dc.SetTextColor(color_Lost);
			dc.SetBkColor(color_Bk);
		}
		else {
			dc.SetTextColor(color_Disk);
			dc.SetBkColor(color_Bk);
		}

		_stprintf(txt, _T("D%d"), i);
		TextOut(dc, hpos_name, txt_y, txt, _tcscnlen(txt, 128));

		if (m_bDiskDestroyed && ((i == m_Destroyed[0]) || (i == m_Destroyed[1])))
			dc.SetBkColor(color_BkLost);

		hex2txt(txt, m_Stripe + i * 4);
		TextOut(dc, hpos_D, txt_y, txt, 64);
		max_D_width = max(max_D_width, dc.GetTextExtent(txt, 64).cx);

		txt_y += v_step;
	}

	if (FALSE == m_bSyndromesCalculated)
		return;

	__int64* P = m_Stripe + m_nDisk * 4;
	__int64* Q = m_Stripe + (m_nDisk + 1) * 4;

	dc.SetTextColor(color_Syndrome);
	dc.SetBkColor(color_Bk);
	TextOut(dc, hpos_name, txt_y, _T("P"), 1);

	hex2txt(txt, P);
	TextOut(dc, hpos_D, txt_y, txt, 64);
	max_D_width = max(max_D_width, dc.GetTextExtent(txt, 64).cx);

	txt_y += v_step;

	TextOut(dc, hpos_name, txt_y, _T("Q"), 1);
	hex2txt(txt, Q);
	TextOut(dc, hpos_D, txt_y, txt, 64);

	max_D_width = max(max_D_width, dc.GetTextExtent(txt, 64).cx);

	if (FALSE == m_bRecovered)
		return;

	hpos_R = hpos_D + max_D_width + mtr.tmMaxCharWidth;

	txt_y = v_step;
	for (int i = 0; i < m_nDisk + 2; i++) {

		if (i == m_Destroyed[0] || i == m_Destroyed[1])
		if (cmp_block(m_Stripe_Recovered + i * 4, m_Stripe + i * 4))
			dc.SetTextColor(color_OK);
		else
			dc.SetTextColor(color_Err);
		else
			dc.SetTextColor(color_Disk);

		hex2txt(txt, m_Stripe_Recovered + i * 4);
		TextOut(dc, hpos_R, txt_y, txt, 64);
		txt_y += v_step;
	}

	// Do not call CWnd::OnPaint() for painting messages
}



void CChildView::OnEasystorageCreate()
{
	// TODO: Add your command handler code here
	if (IDOK != m_EasyStorageDlg.DoModal())
		return;
	if (NULL != m_Stripe)
		delete m_Stripe;
	if (NULL != m_Stripe_Recovered)
		delete m_Stripe_Recovered;
	m_Stripe = new __int64[7 * 4];
	m_Stripe_Recovered = new __int64[7 * 4];
	m_EasyStorageDlg.GetStripe(m_Stripe, m_Destroyed);
	m_nDisk = 5;
	m_bSyndromesCalculated = FALSE;
	m_bDiskDestroyed = FALSE;
	m_bRecovered = FALSE;
	m_bEasyStorage = TRUE;
	m_bTestDone = FALSE;

	Invalidate(1);

}

TCHAR* CChildView::hex2txt(TCHAR* txt, __int64* s)
{
	_stprintf(txt, _T("%016I64X%016I64X%016I64X%016I64X"),
		*(s + 3), *(s + 2), *(s + 1), *s);
	return txt;
}


BOOL CChildView::cmp_block(__int64* a, __int64* b)
{
	for (int i = 0; i < 4; i++)
	if (a[i] != b[i])
		return FALSE;
	return TRUE;
}


__int64* CChildView::zero_block(__int64 *a)
{
	for (int i = 0; i < 4; i++)
		a[i] = 0;
	return a;
}


__int64* CChildView::copy_block(__int64* dst, __int64* src)
{
	memcpy_s(dst, 32, src, 32);
	return dst;
}


__int64* CChildView::rand_block(__int64* block)
{
	__int16 r[16];
	for (int i = 0; i < 16; i++) {
		r[i] = rand();
	}
	memcpy_s(block, 32, r, 32);
	return block;
}


void CChildView::OnUpdateEasystorageCalc(CCmdUI *pCmdUI)
{
	// TODO: Add your command update UI handler code here
	pCmdUI->Enable((m_nDisk != 0) && m_bEasyStorage);
}

#ifdef RDX_STUB // Определено в stdafx.h
extern "C" void CalculateSyndromes_stub(void *D, unsigned int N);
#else
extern "C" void CalculateSyndromes(void *D, unsigned int N);
#endif
void CChildView::OnEasystorageCalc()
{
	// TODO: Add your command handler code here
#ifdef RDX_STUB // Определено в stdafx.h
	CalculateSyndromes_stub(m_Stripe, m_nDisk);
#else
	CalculateSyndromes(m_Stripe, m_nDisk);
#endif
	m_bSyndromesCalculated = TRUE;
	m_bDiskDestroyed = FALSE;
	m_bRecovered = FALSE;
	m_bTestDone = FALSE;
	Invalidate(1);
}


void CChildView::OnUpdateEasystorageDestroy(CCmdUI *pCmdUI)
{
	// TODO: Add your command update UI handler code here
	pCmdUI->Enable(m_bSyndromesCalculated&&m_bEasyStorage);
}


void CChildView::OnEasystorageDestroy()
{
	// TODO: Add your command handler code here
	m_bSyndromesCalculated = TRUE;
	m_bDiskDestroyed = TRUE;
	m_bRecovered = FALSE;
	m_bTestDone = FALSE;

	Invalidate(1);

}
#ifdef RDX_STUB // Определено в stdafx.h
extern "C" void Recover_stub(void *D, unsigned int N, unsigned int a, unsigned int b);
#else
extern "C" void Recover(void *D, unsigned int N, unsigned int a, unsigned int b);
#endif

void CChildView::OnEasystorageRecovertwodrives()
{
	// TODO: Add your command update UI handler code here
	for (int i = 0; i < m_nDisk + 2; i++)
		copy_block(m_Stripe_Recovered + i * 4, m_Stripe + i * 4);
	zero_block(m_Stripe_Recovered + m_Destroyed[0] * 4);
	zero_block(m_Stripe_Recovered + m_Destroyed[1] * 4);
#ifdef RDX_STUB // Определено в stdafx.h
	Recover_stub(m_Stripe_Recovered, m_nDisk, m_Destroyed[0], m_Destroyed[1]);
#else
	Recover(m_Stripe_Recovered, m_nDisk, m_Destroyed[0], m_Destroyed[1]);
#endif

	m_bSyndromesCalculated = TRUE;
	m_bDiskDestroyed = TRUE;
	m_bRecovered = TRUE;
	m_bEasyStorage = TRUE;
	m_bTestDone = FALSE;

	Invalidate(1);
}

void CChildView::OnUpdateEasystorageRecover(CCmdUI *pCmdUI)
{
	pCmdUI->Enable(m_bDiskDestroyed&&m_bEasyStorage);
}


void CChildView::OnRandomstorageCreate()
{
	// TODO: Add your command handler code here
	if (NULL != m_Stripe)
		delete m_Stripe;
	if (NULL != m_Stripe_Recovered)
		delete m_Stripe_Recovered;
	m_nDisk = rand() % (MAX_DISK - MIN_DISK + 1) + MIN_DISK;
	m_Stripe = new __int64[(m_nDisk + 2) * 4];
	m_Stripe_Recovered = new __int64[(m_nDisk + 2) * 4];
	for (int i = 0; i < m_nDisk; i++)
		rand_block(m_Stripe + i * 4);

	m_Destroyed[0] = rand() % (m_nDisk - 1);
	do {
		m_Destroyed[1] = rand() % m_nDisk;
	} while (m_Destroyed[0] >= m_Destroyed[1]);

#ifdef RDX_STUB // Определено в stdafx.h
	CalculateSyndromes_stub(m_Stripe, m_nDisk);
#else
	CalculateSyndromes(m_Stripe, m_nDisk);
#endif

	m_bSyndromesCalculated = TRUE;
	m_bDiskDestroyed = FALSE;
	m_bRecovered = FALSE;
	m_bEasyStorage = FALSE;
	m_bTestDone = FALSE;

	Invalidate(1);
}


void CChildView::OnUpdateRandomstorageRecover(CCmdUI *pCmdUI)
{
	// TODO: Add your command update UI handler code here
	pCmdUI->Enable(m_bSyndromesCalculated && (!m_bEasyStorage));
}


void CChildView::OnRandomstorageRecover()
{
	// TODO: Add your command handler code here
	for (int i = 0; i < m_nDisk + 2; i++)
		copy_block(m_Stripe_Recovered + i * 4, m_Stripe + i * 4);
	zero_block(m_Stripe_Recovered + m_Destroyed[0] * 4);
	zero_block(m_Stripe_Recovered + m_Destroyed[1] * 4);

#ifdef RDX_STUB // Определено в stdafx.h
	Recover_stub(m_Stripe_Recovered, m_nDisk, m_Destroyed[0], m_Destroyed[1]);
#else
	Recover(m_Stripe_Recovered, m_nDisk, m_Destroyed[0], m_Destroyed[1]);
#endif

	m_bSyndromesCalculated = TRUE;
	m_bDiskDestroyed = TRUE;
	m_bRecovered = TRUE;
	m_bEasyStorage = FALSE;
	m_bTestDone = FALSE;

	Invalidate(1);
}


void CChildView::OnTest()
{
	// TODO: Add your command handler code here
	m_bTestDone = FALSE;
	m_bSyndromesCalculated = FALSE;
	m_bDiskDestroyed = FALSE;
	m_bRecovered = FALSE;
	m_bEasyStorage = FALSE;
	m_bTestFailed = FALSE;

	for (WPARAM i = MIN_DISK; i <= MAX_DISK + 1; i++)
		PostMessage(WM_TEST, i);
}




afx_msg LRESULT CChildView::OnTestMessage(WPARAM wParam, LPARAM lParam)
{
	if (m_bTestFailed)
		return 0;
	if (NULL != m_Stripe)
		delete m_Stripe;
	if (NULL != m_Stripe_Recovered)
		delete m_Stripe_Recovered;

	m_nDisk = wParam;
	m_Stripe = new __int64[(m_nDisk + 2) * 4];
	m_Stripe_Recovered = new __int64[(m_nDisk + 2) * 4];
	for (int i = 0; i < m_nDisk; i++)
		rand_block(m_Stripe + i * 4);

	m_Destroyed[0] = rand() % (m_nDisk-1);
	do {
		m_Destroyed[1] = rand() % m_nDisk;
	} while (m_Destroyed[0] >= m_Destroyed[1]);

	__int64 Start = __rdtsc();
#ifdef RDX_STUB // Определено в stdafx.h
	CalculateSyndromes_stub(m_Stripe, m_nDisk);
#else
	CalculateSyndromes(m_Stripe, m_nDisk);
#endif
	__int64 Stop = __rdtsc();
	calctime[m_nDisk] = Stop - Start;

	for (int i = 0; i < m_nDisk + 2; i++)
		copy_block(m_Stripe_Recovered + i * 4, m_Stripe + i * 4);
	zero_block(m_Stripe_Recovered + m_Destroyed[0] * 4);
	zero_block(m_Stripe_Recovered + m_Destroyed[1] * 4);

	Start = __rdtsc();
#ifdef RDX_STUB // Определено в stdafx.h
	Recover_stub(m_Stripe_Recovered, m_nDisk, m_Destroyed[0], m_Destroyed[1]);
#else
	Recover(m_Stripe_Recovered, m_nDisk, m_Destroyed[0], m_Destroyed[1]);
#endif
	Stop = __rdtsc();
	rectime[m_nDisk] = Stop - Start;

	if (!(cmp_block(m_Stripe_Recovered + m_Destroyed[0] * 4, m_Stripe + m_Destroyed[0] * 4)
		&& cmp_block(m_Stripe_Recovered + m_Destroyed[1] * 4, m_Stripe + m_Destroyed[1] * 4))) {
		m_bTestDone = FALSE;
		m_bTestFailed = TRUE;
		m_bSyndromesCalculated = FALSE;
		m_bDiskDestroyed = FALSE;
		m_bRecovered = FALSE;
		m_nDisk = 0;
		MessageBox(_T("Recovered data block is not equal to the lost block"),
			_T("Test failed"), MB_ICONERROR | MB_OK);
		Invalidate(1);
		return 0;
	}
	if (MAX_DISK == wParam) {
		m_bTestDone = TRUE;
		Invalidate(1);
	}
	return 0;
}
