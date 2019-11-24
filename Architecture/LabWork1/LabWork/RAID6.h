
// RAID6.h : main header file for the RAID6 application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CRAID6App:
// See RAID6.cpp for the implementation of this class
//

class CRAID6App : public CWinApp
{
public:
	CRAID6App();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CRAID6App theApp;
