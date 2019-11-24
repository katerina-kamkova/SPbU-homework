
// ChildView.h : interface of the CChildView class
//


#pragma once
#include "RAID6_classes.h"
#define MAX_DISK 30
#define MIN_DISK 5

// CChildView window
#define WM_TEST (WM_USER+2)

class CChildView : public CWnd
{
// Construction
public:
	CChildView();

// Attributes
public:

// Operations
public:

// Overrides
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

// Implementation
public:
	virtual ~CChildView();

	// Generated message map functions
protected:
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()
private:
	CCreateEasyStorageDlg m_EasyStorageDlg;
	__int64* m_Stripe;
	__int64* m_Stripe_Recovered;
	int m_nDisk;
	int m_Destroyed[2];
	__int64 calctime[MAX_DISK + 1];
	__int64 rectime[MAX_DISK + 1];
	BOOL m_bSyndromesCalculated;
	BOOL m_bDiskDestroyed;
	BOOL m_bRecovered;
	BOOL m_bEasyStorage;
	BOOL m_bTestDone;
	BOOL m_bTestFailed;

	static TCHAR* hex2txt(TCHAR* txt, __int64* s);
	static BOOL cmp_block(__int64* a, __int64* b);
	static __int64* zero_block(__int64* a);
	static __int64* copy_block(__int64* dst, __int64* src);
	static __int64* rand_block(__int64* block);

public:
	afx_msg void OnEasystorageCreate();
	afx_msg void OnUpdateEasystorageCalc(CCmdUI *pCmdUI);
	afx_msg void OnEasystorageCalc();
	afx_msg void OnUpdateEasystorageDestroy(CCmdUI *pCmdUI);
	afx_msg void OnEasystorageDestroy();
	afx_msg void OnUpdateEasystorageRecover(CCmdUI *pCmdUI);
	afx_msg void OnEasystorageRecovertwodrives();
	afx_msg void OnRandomstorageCreate();
	afx_msg void OnUpdateRandomstorageRecover(CCmdUI *pCmdUI);
	afx_msg void OnRandomstorageRecover();
	afx_msg void OnTest();
protected:
	afx_msg LRESULT OnTestMessage(WPARAM wParam, LPARAM lParam);
};

