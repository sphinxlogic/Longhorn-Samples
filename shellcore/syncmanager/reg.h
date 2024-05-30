//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------


#ifndef _REGROUTINES_
#define _REGROUTINES_


#define MAX_STRING_LENGTH 256

BOOL SetRegKeyValue(HKEY hKeyTop,LPTSTR pszKey,LPTSTR pszSubkey,LPTSTR pszValue);
BOOL AddRegNamedValue(LPTSTR pszKey,LPTSTR pszSubkey,LPTSTR pszValueName,LPTSTR pszValue);
STDMETHODIMP Reg_SetItemSettingsForHandlerItem(CLSID CLSIDServer, ItemSettings itmstgs);
HKEY CreateHandlerPrefKey(CLSID CLSIDServer);
HKEY CreateHandlerItemPrefKey(CLSID CLSIDServer, SYNCMGRITEMID ItemID);
void Reg_DeleteHandlerPrefKey(CLSID CLSIDServer);
STDMETHODIMP Reg_SetUpRequestedDummySyncItems(DWORD dwci, CLSID CLSIDServer);


#endif // _REGROUTINES_
