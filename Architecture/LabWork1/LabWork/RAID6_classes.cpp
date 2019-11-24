// RAID6_classes.cpp : implementation file
//

#include "stdafx.h"
#include "RAID6.h"
#include "RAID6_classes.h"
#include "afxdialogex.h"

// Проверяет, все ли символы являются шестнадцатеричными символами, и если да,
// возвращает длину строки. Если нет, возвращает 0.
int CAvxEdit::test(TCHAR* txt)
{
	for (int i = 0; i < 64; i++) {
		if (!_istxdigit(txt[i]))
			return 0;
	}
	return (int)_tcsnlen(txt, 127);
}

// Переводит строку txt в __m256i. Проверка корректности txt не производится
__m256i CAvxEdit::txt2avx(TCHAR* txt)
{
	__m256i ret;
	_stscanf(txt, _T("%016I64X%016I64X%016I64X%016I64X"),
		ret.m256i_u64 + 3, ret.m256i_u64 + 2, ret.m256i_u64 + 1, ret.m256i_u64);
	return ret;
}
// Переводит строку txt в __m256i. Проверка корректности txt не производится
TCHAR* CAvxEdit::avx2txt(TCHAR* txt, __m256i avx)
{
	_stprintf(txt, _T("%016I64X%016I64X%016I64X%016I64X"),
		avx.m256i_u64[3], avx.m256i_u64[2], avx.m256i_u64[1], avx.m256i_u64[0]);
	return txt;
}

CAvxEdit::CAvxEdit()
{
	m_Brush.CreateSolidBrush(RGB(255, 255, 255));
	m_Color = RGB(0, 0xFF, 0);
}


CAvxEdit::~CAvxEdit()
{
	if (m_Brush.m_hObject != NULL)
		m_Brush.DeleteObject();
}

void CAvxEdit::SetAvxValue(__m256i a)
{
	TCHAR txt[128];
	_stprintf(txt, _T("%016I64X%016I64X%016I64X%016I64X"),
		a.m256i_u64[3], a.m256i_u64[2], a.m256i_u64[1], a.m256i_u64[0]);
	SetWindowText(txt);
}

__m256i CAvxEdit::GetAvxValue()
{
	TCHAR txt[128];
	__m256i ret = _mm256_set1_epi32(0);
	GetWindowText(txt, 127);

	if (64 == test(txt))
		ret = txt2avx(txt);
	return ret;
}


BOOL CAvxEdit::TestAvxValue()
{
	TCHAR txt[128];
	GetWindowText(txt, 127);
	return(64 == test(txt));
}

BEGIN_MESSAGE_MAP(CAvxEdit, CEdit)
	//ON_WM_CTLCOLOR()
	ON_CONTROL_REFLECT(EN_CHANGE, &CAvxEdit::OnEnChange)
	ON_WM_CTLCOLOR_REFLECT()
END_MESSAGE_MAP()

void CAvxEdit::OnEnChange()
{
	// TODO:  If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CEdit::OnInitDialog()
	// function and call CRichEditCtrl().SetEventMask()
	// with the ENM_CHANGE flag ORed into the mask.

	// TODO:  Add your control notification handler code here
	int sstart, send;
	TCHAR txt[128];
	GetSel(sstart, send);
	GetWindowText(txt, 127);
	int sz = _tcsnlen(txt, 127);
	for (int i = sz; i >= 64; i--)
	if (_T('?') == txt[i])
		txt[i] = NULL;
	for (int i = sz; i < 64; i++)
		_tcscat(txt, _T("?"));

// Эта ошибка жила много лет и приводила к нехватке памяти в виртуальной
//   среде. Две следующие строки оставлены в назидание. 
//	SetWindowText(txt);
//	SetSel(sstart, send);

	LPARAM isNumberOK;
	if (64 == test(txt))
		isNumberOK = (LPARAM)TRUE;
	else
		isNumberOK = (LPARAM)FALSE;
	GetParent()->SendMessage(WM_AVXEDIT_CHANGE, (WPARAM)m_hWnd, isNumberOK);
}


HBRUSH CAvxEdit::CtlColor(CDC* pDC, UINT /*nCtlColor*/)
{
	// TODO:  Change any attributes of the DC here
	TCHAR txt[128];
	GetWindowText(txt, 127);
	if (64 != test(txt))
		pDC->SetTextColor(RGB(0xF0, 0, 0x10));
	else
		pDC->SetTextColor(RGB(0, 0, 0));

	return m_Brush;
}




// CCreateEasyStorageDlg dialog

IMPLEMENT_DYNAMIC(CCreateEasyStorageDlg, CDialogEx)

CCreateEasyStorageDlg::CCreateEasyStorageDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CCreateEasyStorageDlg::IDD, pParent)
{

}

CCreateEasyStorageDlg::~CCreateEasyStorageDlg()
{
}

void CCreateEasyStorageDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_D0, m_D0_Edit);
	DDX_Control(pDX, IDC_EDIT_D1, m_D1_Edit);
	DDX_Control(pDX, IDC_EDIT_D2, m_D2_Edit);
	DDX_Control(pDX, IDC_EDIT_D3, m_D3_Edit);
	DDX_Control(pDX, IDC_EDIT_D4, m_D4_Edit);
	DDX_Control(pDX, IDC_CHECK_0, m_D0_btn);
	DDX_Control(pDX, IDC_CHECK_1, m_D1_btn);
	DDX_Control(pDX, IDC_CHECK_2, m_D2_btn);
	DDX_Control(pDX, IDC_CHECK_3, m_D3_btn);
	DDX_Control(pDX, IDC_CHECK_4, m_D4_btn);
	DDX_Control(pDX, IDOK, m_btnOK);
}


BEGIN_MESSAGE_MAP(CCreateEasyStorageDlg, CDialogEx)
	ON_MESSAGE(WM_AVXEDIT_CHANGE, &CCreateEasyStorageDlg::OnAvxeditChange)
	ON_BN_CLICKED(IDC_CHECK_0, &CCreateEasyStorageDlg::OnBnClickedCheck0)
	ON_BN_CLICKED(IDC_CHECK_1, &CCreateEasyStorageDlg::OnBnClickedCheck1)
	ON_BN_CLICKED(IDC_CHECK_2, &CCreateEasyStorageDlg::OnBnClickedCheck2)
	ON_BN_CLICKED(IDC_CHECK_3, &CCreateEasyStorageDlg::OnBnClickedCheck3)
	ON_BN_CLICKED(IDC_CHECK_4, &CCreateEasyStorageDlg::OnBnClickedCheck4)
	ON_BN_CLICKED(IDOK, &CCreateEasyStorageDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// CCreateEasyStorageDlg message handlers



BOOL CCreateEasyStorageDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// TODO:  Add extra initialization here
	for (int i = 0; i < 5; i++)
		m_D[i] = _mm256_set1_epi8(5 - i);
	m_Destroyed[0] = 0;
	m_Destroyed[1] = 1;

	m_D0_Edit.SetAvxValue(m_D[0]);
	m_D1_Edit.SetAvxValue(m_D[1]);
	m_D2_Edit.SetAvxValue(m_D[2]);
	m_D3_Edit.SetAvxValue(m_D[3]);
	m_D4_Edit.SetAvxValue(m_D[4]);

	m_D0_btn.SetCheck(BST_CHECKED);
	m_D1_btn.SetCheck(BST_CHECKED);


	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


afx_msg LRESULT CCreateEasyStorageDlg::OnAvxeditChange(WPARAM wParam, LPARAM lParam)
{
	BOOL EnableOK = TRUE;
	if (!m_D0_Edit.TestAvxValue()) EnableOK = FALSE;
	if (!m_D1_Edit.TestAvxValue()) EnableOK = FALSE;
	if (!m_D2_Edit.TestAvxValue()) EnableOK = FALSE;
	if (!m_D3_Edit.TestAvxValue()) EnableOK = FALSE;
	if (!m_D4_Edit.TestAvxValue()) EnableOK = FALSE;
	m_btnOK.EnableWindow(EnableOK);

	return 0;
}


CButton* CCreateEasyStorageDlg::GetButtonPtr(int no)
{
	switch (no){
	case 0:
		return &m_D0_btn;
	case 1:
		return &m_D1_btn;
	case 2:
		return &m_D2_btn;
	case 3:
		return &m_D3_btn;
	case 4:
		return &m_D4_btn;
	default:
		return NULL;
	}
}


__int64* CCreateEasyStorageDlg::GetStripe(__int64* Stripe, int* Destroyed)
{
	memcpy_s(Stripe, 5 * 32, m_D, 5 * 32);
	Destroyed[0] = m_Destroyed[0];
	Destroyed[1] = m_Destroyed[1];
	return Stripe;
}


void CCreateEasyStorageDlg::OnBnClickedCheck0()
{
	// TODO: Add your control notification handler code here
	if (m_Destroyed[0] == 0 || m_Destroyed[1] == 0)
		m_D0_btn.SetCheck(BST_CHECKED);
	else {
		CButton *pD = GetButtonPtr(m_Destroyed[0]);
		pD->SetCheck(BST_UNCHECKED);
		m_Destroyed[0] = m_Destroyed[1];
		m_Destroyed[1] = 0;
	}
}


void CCreateEasyStorageDlg::OnBnClickedCheck1()
{
	// TODO: Add your control notification handler code here
	if (m_Destroyed[0] == 1 || m_Destroyed[1] == 1)
		m_D1_btn.SetCheck(BST_CHECKED);
	else {
		CButton *pD = GetButtonPtr(m_Destroyed[0]);
		pD->SetCheck(BST_UNCHECKED);
		m_Destroyed[0] = m_Destroyed[1];
		m_Destroyed[1] = 1;
	}
}


void CCreateEasyStorageDlg::OnBnClickedCheck2()
{
	// TODO: Add your control notification handler code here
	if (m_Destroyed[0] == 2 || m_Destroyed[1] == 2)
		m_D2_btn.SetCheck(BST_CHECKED);
	else {
		CButton *pD = GetButtonPtr(m_Destroyed[0]);
		pD->SetCheck(BST_UNCHECKED);
		m_Destroyed[0] = m_Destroyed[1];
		m_Destroyed[1] = 2;
	}
}


void CCreateEasyStorageDlg::OnBnClickedCheck3()
{
	// TODO: Add your control notification handler code here
	if (m_Destroyed[0] == 3 || m_Destroyed[1] == 3)
		m_D3_btn.SetCheck(BST_CHECKED);
	else {
		CButton *pD = GetButtonPtr(m_Destroyed[0]);
		pD->SetCheck(BST_UNCHECKED);
		m_Destroyed[0] = m_Destroyed[1];
		m_Destroyed[1] = 3;
	}
}


void CCreateEasyStorageDlg::OnBnClickedCheck4()
{
	// TODO: Add your control notification handler code here
	// TODO: Add your control notification handler code here
	if (m_Destroyed[0] == 4 || m_Destroyed[1] == 4)
		m_D4_btn.SetCheck(BST_CHECKED);
	else {
		CButton *pD = GetButtonPtr(m_Destroyed[0]);
		pD->SetCheck(BST_UNCHECKED);
		m_Destroyed[0] = m_Destroyed[1];
		m_Destroyed[1] = 4;
	}
}


void CCreateEasyStorageDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here
	m_D[0] = m_D0_Edit.GetAvxValue();
	m_D[1] = m_D1_Edit.GetAvxValue();
	m_D[2] = m_D2_Edit.GetAvxValue();
	m_D[3] = m_D3_Edit.GetAvxValue();
	m_D[4] = m_D4_Edit.GetAvxValue();
	if (m_Destroyed[0] > m_Destroyed[1]) {
		int tmp = m_Destroyed[0];
		m_Destroyed[0] = m_Destroyed[1];
		m_Destroyed[1] = tmp;
	}
	CDialogEx::OnOK();
}
