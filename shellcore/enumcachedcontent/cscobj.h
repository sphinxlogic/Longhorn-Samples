

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0401 */
/* Compiler settings for cscproxy.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __cscproxy_h__
#define __cscproxy_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IOfflineFilesConnectionPoint_FWD_DEFINED__
#define __IOfflineFilesConnectionPoint_FWD_DEFINED__
typedef interface IOfflineFilesConnectionPoint IOfflineFilesConnectionPoint;
#endif 	/* __IOfflineFilesConnectionPoint_FWD_DEFINED__ */


#ifndef __IOfflineFilesEvents_FWD_DEFINED__
#define __IOfflineFilesEvents_FWD_DEFINED__
typedef interface IOfflineFilesEvents IOfflineFilesEvents;
#endif 	/* __IOfflineFilesEvents_FWD_DEFINED__ */


#ifndef __IOfflineFilesProgress_FWD_DEFINED__
#define __IOfflineFilesProgress_FWD_DEFINED__
typedef interface IOfflineFilesProgress IOfflineFilesProgress;
#endif 	/* __IOfflineFilesProgress_FWD_DEFINED__ */


#ifndef __IOfflineFilesSimpleProgress_FWD_DEFINED__
#define __IOfflineFilesSimpleProgress_FWD_DEFINED__
typedef interface IOfflineFilesSimpleProgress IOfflineFilesSimpleProgress;
#endif 	/* __IOfflineFilesSimpleProgress_FWD_DEFINED__ */


#ifndef __IOfflineFilesSyncProgress_FWD_DEFINED__
#define __IOfflineFilesSyncProgress_FWD_DEFINED__
typedef interface IOfflineFilesSyncProgress IOfflineFilesSyncProgress;
#endif 	/* __IOfflineFilesSyncProgress_FWD_DEFINED__ */


#ifndef __IOfflineFilesCacheMoveProgress_FWD_DEFINED__
#define __IOfflineFilesCacheMoveProgress_FWD_DEFINED__
typedef interface IOfflineFilesCacheMoveProgress IOfflineFilesCacheMoveProgress;
#endif 	/* __IOfflineFilesCacheMoveProgress_FWD_DEFINED__ */


#ifndef __IOfflineFilesSyncConflictHandler_FWD_DEFINED__
#define __IOfflineFilesSyncConflictHandler_FWD_DEFINED__
typedef interface IOfflineFilesSyncConflictHandler IOfflineFilesSyncConflictHandler;
#endif 	/* __IOfflineFilesSyncConflictHandler_FWD_DEFINED__ */


#ifndef __IOfflineFile_FWD_DEFINED__
#define __IOfflineFile_FWD_DEFINED__
typedef interface IOfflineFile IOfflineFile;
#endif 	/* __IOfflineFile_FWD_DEFINED__ */


#ifndef __IEnumOfflineFiles_FWD_DEFINED__
#define __IEnumOfflineFiles_FWD_DEFINED__
typedef interface IEnumOfflineFiles IEnumOfflineFiles;
#endif 	/* __IEnumOfflineFiles_FWD_DEFINED__ */


#ifndef __IEnumOfflineFilesData_FWD_DEFINED__
#define __IEnumOfflineFilesData_FWD_DEFINED__
typedef interface IEnumOfflineFilesData IEnumOfflineFilesData;
#endif 	/* __IEnumOfflineFilesData_FWD_DEFINED__ */


#ifndef __IOfflineFilesSetting_FWD_DEFINED__
#define __IOfflineFilesSetting_FWD_DEFINED__
typedef interface IOfflineFilesSetting IOfflineFilesSetting;
#endif 	/* __IOfflineFilesSetting_FWD_DEFINED__ */


#ifndef __IOfflineFilesSyncControl_FWD_DEFINED__
#define __IOfflineFilesSyncControl_FWD_DEFINED__
typedef interface IOfflineFilesSyncControl IOfflineFilesSyncControl;
#endif 	/* __IOfflineFilesSyncControl_FWD_DEFINED__ */


#ifndef __IOfflineFilesCache_FWD_DEFINED__
#define __IOfflineFilesCache_FWD_DEFINED__
typedef interface IOfflineFilesCache IOfflineFilesCache;
#endif 	/* __IOfflineFilesCache_FWD_DEFINED__ */


#ifndef __IOfflineFilesServiceCallback_FWD_DEFINED__
#define __IOfflineFilesServiceCallback_FWD_DEFINED__
typedef interface IOfflineFilesServiceCallback IOfflineFilesServiceCallback;
#endif 	/* __IOfflineFilesServiceCallback_FWD_DEFINED__ */


#ifndef __IOfflineFilesService_FWD_DEFINED__
#define __IOfflineFilesService_FWD_DEFINED__
typedef interface IOfflineFilesService IOfflineFilesService;
#endif 	/* __IOfflineFilesService_FWD_DEFINED__ */


#ifndef __AsyncIOfflineFilesService_FWD_DEFINED__
#define __AsyncIOfflineFilesService_FWD_DEFINED__
typedef interface AsyncIOfflineFilesService AsyncIOfflineFilesService;
#endif 	/* __AsyncIOfflineFilesService_FWD_DEFINED__ */


#ifndef __OfflineFilesService_FWD_DEFINED__
#define __OfflineFilesService_FWD_DEFINED__

#ifdef __cplusplus
typedef class OfflineFilesService OfflineFilesService;
#else
typedef struct OfflineFilesService OfflineFilesService;
#endif /* __cplusplus */

#endif 	/* __OfflineFilesService_FWD_DEFINED__ */


#ifndef __OfflineFilesCache_FWD_DEFINED__
#define __OfflineFilesCache_FWD_DEFINED__

#ifdef __cplusplus
typedef class OfflineFilesCache OfflineFilesCache;
#else
typedef struct OfflineFilesCache OfflineFilesCache;
#endif /* __cplusplus */

#endif 	/* __OfflineFilesCache_FWD_DEFINED__ */


/* header files for imported files */
#include "oleidl.h"
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

/* interface __MIDL_itf_cscproxy_0000 */
/* [local] */ 

//+-------------------------------------------------------------------------
//
//  Microsoft Windows
//  Copyright 2003 Microsoft Corporation. All Rights Reserved.
//
//--------------------------------------------------------------------------
//
#ifdef CSCOBJ_EXPORTS
#define CSCOBJ_API __declspec(dllexport)
#else
#define CSCOBJ_API __declspec(dllimport)
#endif
// {48C6BE7C-3871-43cc-B46F-1449A1BB2FF3}
DEFINE_GUID(CLSID_OfflineFilesCache,
0x48c6be7c, 0x3871, 0x43cc, 0xb4, 0x6f, 0x14, 0x49, 0xa1, 0xbb, 0x2f, 0xf3);
// {B463502C-9A67-420c-9031-BAE5E3B65867}
DEFINE_GUID(IID_IOfflineFilesCache,
0xb463502c, 0x9a67, 0x420c, 0x90, 0x31, 0xba, 0xe5, 0xe3, 0xb6, 0x58, 0x67);
// {C203411A-F387-40ee-A88A-115EDBC829F2}
DEFINE_GUID(IID_IOfflineFilesConnectionPoint,
0xc203411a, 0xf387, 0x40ee, 0xa8, 0x8a, 0x11, 0x5e, 0xdb, 0xc8, 0x29, 0xf2);
// {36349D82-1B04-4742-B992-533D0BB5DD82}
DEFINE_GUID(IID_IOfflineFilesEvents,
0x36349d82, 0x1b04, 0x4742, 0xb9, 0x92, 0x53, 0x3d, 0xb, 0xb5, 0xdd, 0x82);
// {734C6091-A6D2-4908-9B2F-09C880DDAD78}
DEFINE_GUID(IID_IOfflineFilesSyncProgress,
0x734c6091, 0xa6d2, 0x4908, 0x9b, 0x2f, 0x9, 0xc8, 0x80, 0xdd, 0xad, 0x78);
// {3F91289A-10BA-41fd-B85B-11C5F9F62784}
DEFINE_GUID(IID_IOfflineFilesSyncControl,
0x3f91289a, 0x10ba, 0x41fd, 0xb8, 0x5b, 0x11, 0xc5, 0xf9, 0xf6, 0x27, 0x84);
// {B6DD5092-C65C-46b6-97B8-FADD08E7E1BE}
DEFINE_GUID(IID_IOfflineFilesSyncConflictHandler, 
0xb6dd5092, 0xc65c, 0x46b6, 0x97, 0xb8, 0xfa, 0xdd, 0x8, 0xe7, 0xe1, 0xbe);
// {FAD63237-C55B-4911-9850-BCF96D4C979E}
DEFINE_GUID(IID_IOfflineFilesProgress,
0xfad63237, 0xc55b, 0x4911, 0x98, 0x50, 0xbc, 0xf9, 0x6d, 0x4c, 0x97, 0x9e);
// {C34F7F9B-C43D-4f9d-A776-C0EB6DE5D401}
DEFINE_GUID(IID_IOfflineFilesSimpleProgress,
0xc34f7f9b, 0xc43d, 0x4f9d, 0xa7, 0x76, 0xc0, 0xeb, 0x6d, 0xe5, 0xd4, 0x1);
// {7DB89040-B032-4a28-B1CB-A04C45AFB374}
DEFINE_GUID(IID_IOfflineFilesCacheMoveProgress,
0x7db89040, 0xb032, 0x4a28, 0xb1, 0xcb, 0xa0, 0x4c, 0x45, 0xaf, 0xb3, 0x74);
// {60BC9146-13FC-4110-A1E2-93F8D52F1AD9}
DEFINE_GUID(IID_IEnumOfflineFiles,
0x60bc9146, 0x13fc, 0x4110, 0xa1, 0xe2, 0x93, 0xf8, 0xd5, 0x2f, 0x1a, 0xd9);
// {AE3D3247-2D8D-40ce-B76E-6C3124831B79}
DEFINE_GUID(IID_IOfflineFile,
0xae3d3247, 0x2d8d, 0x40ce, 0xb7, 0x6e, 0x6c, 0x31, 0x24, 0x83, 0x1b, 0x79);
// {15F277C6-8600-4941-B47C-FD9F332ADD95}
DEFINE_GUID(IID_IOfflineFilesSetting,
0x15f277c6, 0x8600, 0x4941, 0xb4, 0x7c, 0xfd, 0x9f, 0x33, 0x2a, 0xdd, 0x95);
//
// The event class ordinals.
// Each entry identifies one class of subscription events
// delivered to clients by the Offline Files service.  Values are used
// for the dwClassCode member of OFFLINEFILES_EVENT_FILTER in calls
// to IOfflineFilesCache::RegisterEventConnection or
// IOfflineFilesConnectionPoint::AdviseWithFilter.
//
// OFFLINEFILES_EVENTCLASS_CACHE - Events about cache status
// OFFLINEFILES_EVENTCLASS_FILE  - Events about file operations and status
// OFFLINEFILES_EVENTCLASS_SYNC  - Events fired during a sync operation
//
typedef 
enum tagOFFLINEFILES_EVENT_CLASSES
    {	OFFLINEFILES_EVENTCLASS_MANDATORY	= 0,
	OFFLINEFILES_EVENTCLASS_CACHE	= OFFLINEFILES_EVENTCLASS_MANDATORY + 1,
	OFFLINEFILES_EVENTCLASS_FILE	= OFFLINEFILES_EVENTCLASS_CACHE + 1,
	OFFLINEFILES_EVENTCLASS_SYNC	= OFFLINEFILES_EVENTCLASS_FILE + 1,
	OFFLINEFILES_EVENTCLASS_CSCUI	= OFFLINEFILES_EVENTCLASS_SYNC + 1,
	OFFLINEFILES_NUM_EVENT_CLASSES	= OFFLINEFILES_EVENTCLASS_CSCUI + 1
    } 	OFFLINEFILES_EVENT_CLASSES;

//
// Identifiers for individual subscription events.
// Each event is identified by a particular bit in a 32-bit mask.
// The bit numbers are known only within a particular event class
// such that there is a maximum of 32 events per event class and a 
// maximum of 32 x OFFLINEFILES_NUM_EVENT_CLASSES total events.
//
#define	OFFLINEFILES_EVENTMASK_MANDATORY_PING	( 0x1 )

#define	OFFLINEFILES_EVENTMASK_MANDATORY_DATALOST	( 0x2 )

#define	OFFLINEFILES_EVENTMASK_MANDATORY_ALL	( 0x3 )

#define	OFFLINEFILES_EVENTMASK_CACHE_ENABLED	( 0x1 )

#define	OFFLINEFILES_EVENTMASK_CACHE_DISABLED	( 0x2 )

#define	OFFLINEFILES_EVENTMASK_CACHE_ENCRYPTIONCHANGED	( 0x4 )

#define	OFFLINEFILES_EVENTMASK_CACHE_MOVED	( 0x8 )

#define	OFFLINEFILES_EVENTMASK_CACHE_ALL	( 0xf )

#define	OFFLINEFILES_EVENTMASK_FILE_PINNED	( 0x1 )

#define	OFFLINEFILES_EVENTMASK_FILE_UNPINNED	( 0x2 )

#define	OFFLINEFILES_EVENTMASK_FILE_SYNCHRONIZED	( 0x4 )

#define	OFFLINEFILES_EVENTMASK_FILE_RENAMED	( 0x8 )

#define	OFFLINEFILES_EVENTMASK_FILE_DELETED	( 0x10 )

#define	OFFLINEFILES_EVENTMASK_FILE_ALL	( 0x1f )

#define	OFFLINEFILES_EVENTMASK_SYNC_BEGINSYNC	( 0x1 )

#define	OFFLINEFILES_EVENTMASK_SYNC_FILERESULT	( 0x2 )

#define	OFFLINEFILES_EVENTMASK_SYNC_ENDSYNC	( 0x4 )

#define	OFFLINEFILES_EVENTMASK_SYNC_ALL	( 0x7 )

#define	OFFLINEFILES_EVENTMASK_CSCUI_INITIALIZE	( 0x1 )

#define	OFFLINEFILES_EVENTMASK_CSCUI_NETUP	( 0x2 )

#define	OFFLINEFILES_EVENTMASK_CSCUI_NETDOWN	( 0x4 )

#define	OFFLINEFILES_EVENTMASK_CSCUI_CACHECORRUPTED	( 0x8 )

#define	OFFLINEFILES_EVENTMASK_CSCUI_ALL	( 0xf )

//
// Structure passed by a subscriber in IOfflineFilesCache::RegisterEventConnection
// or IOfflineFilesConnectionPoint::AdviseWithFilter.
// when they want to filter on particular events.  Typically a subscriber 
// builds an array of these, one element for each event class they're interested
// in.
//
// dwClassCode  - One of OFFLINEFILES_EVENTCLASS_XXXXX
// dwEventMask  - One or more of OFFLINEFILES_EVENTMASK_XXXX
//
typedef struct tagOFFLINEFILES_EVENT_FILTER
    {
    DWORD dwClassCode;
    DWORD dwEventMask;
    } 	OFFLINEFILES_EVENT_FILTER;

//
// Code provided to IOfflineFilesCache::RegisterEventConnection along with
// a "path filter" string when event path filtering is desired by the 
// registered client.  For those types of events generated for individual
// files and directories, path filtering prevents clients from receiving
// events on files and directories they are not interested in.  The 
// OFFLINEFILES_PATHFILTER_MATCH value controls how the filter string
// provided is interpreted at runtime.
//
// OFFLINEFILES_PATHFILTER_SELF             - Exact match
// OFFLINEFILES_PATHFILTER_CHILD            - Immediate children only
// OFFLINEFILES_PATHFILTER_DESCENDENT       - Any descendent
// OFFLINEFILES_PATHFILTER_SELFORCHILD      - Exact match or immediate child
// OFFLINEFILES_PATHFILTER_SELFORDESCENDENT - Exact match or any descendent
//
typedef 
enum tagOFFLINEFILES_PATHFILTER_MATCH
    {	OFFLINEFILES_PATHFILTER_SELF	= 0,
	OFFLINEFILES_PATHFILTER_CHILD	= OFFLINEFILES_PATHFILTER_SELF + 1,
	OFFLINEFILES_PATHFILTER_DESCENDENT	= OFFLINEFILES_PATHFILTER_CHILD + 1,
	OFFLINEFILES_PATHFILTER_SELFORCHILD	= OFFLINEFILES_PATHFILTER_DESCENDENT + 1,
	OFFLINEFILES_PATHFILTER_SELFORDESCENDENT	= OFFLINEFILES_PATHFILTER_SELFORCHILD + 1
    } 	OFFLINEFILES_PATHFILTER_MATCH;

//
// This enumeration describes the various types of caching
// behavior available for cached directories.
//
typedef 
enum tagOFFLINEFILES_CACHING_MODE
    {	OFFLINEFILES_CACHING_MODE_NONE	= 0,
	OFFLINEFILES_CACHING_MODE_NOCACHING	= OFFLINEFILES_CACHING_MODE_NONE + 1,
	OFFLINEFILES_CACHING_MODE_MANUAL	= OFFLINEFILES_CACHING_MODE_NOCACHING + 1,
	OFFLINEFILES_CACHING_MODE_AUTOMATIC	= OFFLINEFILES_CACHING_MODE_MANUAL + 1
    } 	OFFLINEFILES_CACHING_MODE;

//
// This enumeration describes the various types of synchronization
// behavior available for cached directories.
//
typedef 
enum tagOFFLINEFILES_SYNC_MODE
    {	OFFLINEFILES_SYNC_MODE_NONE	= 0,
	OFFLINEFILES_SYNC_MODE_SUSPENDED	= OFFLINEFILES_SYNC_MODE_NONE + 1,
	OFFLINEFILES_SYNC_MODE_IN	= OFFLINEFILES_SYNC_MODE_SUSPENDED + 1,
	OFFLINEFILES_SYNC_MODE_OUT	= OFFLINEFILES_SYNC_MODE_IN + 1
    } 	OFFLINEFILES_SYNC_MODE;

//
// User response code returned to the service by some of the progress
// callback methods.
// Many of the progress callback methods allow the client's progress
// implementation to abort a lengthy operation or to retry a failed operation.
//
typedef 
enum tagOFFLINEFILES_OP_RESPONSE
    {	OFFLINEFILES_OP_CONTINUE	= 0,
	OFFLINEFILES_OP_RETRY	= OFFLINEFILES_OP_CONTINUE + 1,
	OFFLINEFILES_OP_ABORT	= OFFLINEFILES_OP_RETRY + 1
    } 	OFFLINEFILES_OP_RESPONSE;

//
// This enumeration describes the various attributes attached
// to files and directories stored in the Offline Files cache.
// Use IOfflineFile::GetStatus to obtain the status for a 
// particular file or directory.
//
typedef 
enum tagOFFLINEFILES_FILE_STATUS
    {	OFFLINEFILES_FILE_STATUS_DIRECTORY	= 0x1,
	OFFLINEFILES_FILE_STATUS_SHAREROOT	= 0x2,
	OFFLINEFILES_FILE_STATUS_LOCALLYMODIFIEDDATA	= 0x4,
	OFFLINEFILES_FILE_STATUS_LOCALLYMODIFIEDATTR	= 0x8,
	OFFLINEFILES_FILE_STATUS_LOCALLYMODIFIEDTIME	= 0x10,
	OFFLINEFILES_FILE_STATUS_CREATEDOFFLINE	= 0x20,
	OFFLINEFILES_FILE_STATUS_SPARSE	= 0x40,
	OFFLINEFILES_FILE_STATUS_ORPHAN	= 0x80,
	OFFLINEFILES_FILE_STATUS_USER_PINNED	= 0x100,
	OFFLINEFILES_FILE_STATUS_SYSTEM_PINNED	= 0x200,
	OFFLINEFILES_FILE_STATUS_USER_INHERIT_PINNED	= 0x400,
	OFFLINEFILES_FILE_STATUS_SYSTEM_INHERIT_PINNED	= 0x800
    } 	OFFLINEFILES_FILE_STATUS;

//
// This enumeration describes the various attributes attached
// to cached network shares.
// Use IOfflineFile::GetShareStatus to obtain the status for a 
// particular file or directory's network share cache entry.
//
typedef 
enum tagOFFLINEFILES_SHARE_STATUS
    {	OFFLINEFILES_SHARE_STATUS_MODIFIED_OFFLINE	= 0x1,
	OFFLINEFILES_SHARE_STATUS_CONNECTED	= 0x2,
	OFFLINEFILES_SHARE_STATUS_FILES_OPEN	= 0x4,
	OFFLINEFILES_SHARE_STATUS_FINDS_IN_PROGRESS	= 0x8,
	OFFLINEFILES_SHARE_STATUS_DISCONNECTED_OP	= 0x10,
	OFFLINEFILES_SHARE_STATUS_MERGING	= 0x20
    } 	OFFLINEFILES_SHARE_STATUS;

//
// This structure is used with IOfflineFilesCache::QueryStatistics
// to gather statistics on directories in the cache.
//
typedef struct tagOFFLINEFILES_STATISTICS
    {
    int cTotal;
    int cFiles;
    int cDirs;
    int cShares;
    int cPinnedTotal;
    int cPinnedFiles;
    int cPinnedDirs;
    int cModified;
    int cSparse;
    int cOffline;
    int cAccessUser;
    int cAccessGuest;
    int cAccessOther;
    } 	OFFLINEFILES_STATISTICS;

#define	OFFLINEFILES_STATS_UNITYFLAG_NONE	( 0 )

#define	OFFLINEFILES_STATS_UNITYFLAG_TOTAL	( 0x1 )

#define	OFFLINEFILES_STATS_UNITYFLAG_PINNED	( 0x2 )

#define	OFFLINEFILES_STATS_UNITYFLAG_MODIFIED	( 0x4 )

#define	OFFLINEFILES_STATS_UNITYFLAG_SPARSE	( 0x8 )

#define	OFFLINEFILES_STATS_UNITYFLAG_DIRS	( 0x10 )

#define	OFFLINEFILES_STATS_UNITYFLAG_ACCUSER	( 0x20 )

#define	OFFLINEFILES_STATS_UNITYFLAG_ACCGUEST	( 0x40 )

#define	OFFLINEFILES_STATS_UNITYFLAG_ACCOTHER	( 0x80 )

#define	OFFLINEFILES_STATS_UNITYFLAG_ACCALL	( 0x100 )

#define	OFFLINEFILES_STATS_UNITYFLAG_ALL	( 0xff )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_NONE	( 0 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_LOCAL_MOD_DATA	( 0x1 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_LOCAL_MOD_ATTRIB	( 0x2 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_LOCAL_MOD_TIME	( 0x4 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_LOCAL_DELETED	( 0x8 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_LOCAL_CREATED	( 0x10 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_SPARSE	( 0x20 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_DIRECTORY	( 0x40 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_FILE	( 0x80 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_NOACCUSER	( 0x100 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_NOACCGUEST	( 0x200 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_NOACCOTHER	( 0x400 )

#define	OFFLINEFILES_STATS_EXCLUDEFLAG_NOACCALL	( 0x800 )

//
// IOfflineFilesConnectionPoint interface
//
// Implemented by Windows.
//
// This interface provides a specialization of IConnectionPoint::Advise to
// support event filtering.  The CLSID_OfflineFilesCache object supports
// this event interface from its connection point container.
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0000_v0_0_s_ifspec;

#ifndef __IOfflineFilesConnectionPoint_INTERFACE_DEFINED__
#define __IOfflineFilesConnectionPoint_INTERFACE_DEFINED__

/* interface IOfflineFilesConnectionPoint */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesConnectionPoint;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("C203411A-F387-40ee-A88A-115EDBC829F2")
    IOfflineFilesConnectionPoint : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE AdviseWithFilter( 
            /* [in] */ IUnknown *punkSink,
            /* [out] */ DWORD *pdwCookie,
            /* [size_is][unique][in] */ OFFLINEFILES_EVENT_FILTER *prgEventFilters,
            /* [in] */ int cEventFilters,
            /* [string][unique][in] */ LPCWSTR pszPathFilter,
            /* [in] */ OFFLINEFILES_PATHFILTER_MATCH PathFilterMatch) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesConnectionPointVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesConnectionPoint * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesConnectionPoint * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesConnectionPoint * This);
        
        HRESULT ( STDMETHODCALLTYPE *AdviseWithFilter )( 
            IOfflineFilesConnectionPoint * This,
            /* [in] */ IUnknown *punkSink,
            /* [out] */ DWORD *pdwCookie,
            /* [size_is][unique][in] */ OFFLINEFILES_EVENT_FILTER *prgEventFilters,
            /* [in] */ int cEventFilters,
            /* [string][unique][in] */ LPCWSTR pszPathFilter,
            /* [in] */ OFFLINEFILES_PATHFILTER_MATCH PathFilterMatch);
        
        END_INTERFACE
    } IOfflineFilesConnectionPointVtbl;

    interface IOfflineFilesConnectionPoint
    {
        CONST_VTBL struct IOfflineFilesConnectionPointVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesConnectionPoint_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesConnectionPoint_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesConnectionPoint_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesConnectionPoint_AdviseWithFilter(This,punkSink,pdwCookie,prgEventFilters,cEventFilters,pszPathFilter,PathFilterMatch)	\
    (This)->lpVtbl -> AdviseWithFilter(This,punkSink,pdwCookie,prgEventFilters,cEventFilters,pszPathFilter,PathFilterMatch)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesConnectionPoint_AdviseWithFilter_Proxy( 
    IOfflineFilesConnectionPoint * This,
    /* [in] */ IUnknown *punkSink,
    /* [out] */ DWORD *pdwCookie,
    /* [size_is][unique][in] */ OFFLINEFILES_EVENT_FILTER *prgEventFilters,
    /* [in] */ int cEventFilters,
    /* [string][unique][in] */ LPCWSTR pszPathFilter,
    /* [in] */ OFFLINEFILES_PATHFILTER_MATCH PathFilterMatch);


void __RPC_STUB IOfflineFilesConnectionPoint_AdviseWithFilter_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesConnectionPoint_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0260 */
/* [local] */ 

//
// IOfflineFilesEvents interface.
//
// Implement this interface when you wish to receive events from
// the Offline Files service.
//
// These are the "subscription" events that are delivered to clients
// who have registered through the cache object's connection point container.
//
// Method: CacheMoved
//
// Called when the cache has been successfully moved to a new location.
//
// Method: Enabled
//
// Called when Offline Files has been enabled or disabled.
//
// Method: EncryptionChanged
//
// Called when the encryption state of the cache has changed.  The possible
// states are:
//
//      1. Fully unencrypted
//      2. Partially unencrypted
//      3. Partially encrypted
//      4. Fully encrypted
//
// Method: FileAvailableOffline
//
// Called after a file or directory has been added to the cache.
//
// Method: FileDeleted
//
// Called after a file or directory has been deleted from the cache.
//
// Method: DataLost
//
// Called when one or more notifications have been "dropped" by the service
// as a result of the client not processing notifications.  This will occur
// in the unlikely event the service is generating events faster than clients 
// can accept and process them.  
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0260_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0260_v0_0_s_ifspec;

#ifndef __IOfflineFilesEvents_INTERFACE_DEFINED__
#define __IOfflineFilesEvents_INTERFACE_DEFINED__

/* interface IOfflineFilesEvents */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("36349D82-1B04-4742-B992-533D0BB5DD82")
    IOfflineFilesEvents : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE CacheMoved( 
            /* [string][in] */ LPCWSTR pszOldPath,
            /* [string][in] */ LPCWSTR pszNewPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Enabled( 
            /* [in] */ BOOL bEnabled) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE EncryptionChanged( 
            /* [in] */ BOOL bWasEncrypted,
            /* [in] */ BOOL bWasPartial,
            /* [in] */ BOOL bIsEncrypted,
            /* [in] */ BOOL bIsPartial) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FileAvailableOffline( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bDirectory,
            /* [in] */ BOOL bAvailableOffline) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FileDeleted( 
            /* [string][in] */ LPCWSTR pszPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FilePinned( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bDirectory) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FileRollback( 
            /* [string][in] */ LPCWSTR pszPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FileUnpinned( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bDirectory,
            /* [in] */ BOOL bLocalCopyDeleted) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE PathConnected( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bConnected) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE PathRenamed( 
            /* [string][in] */ LPCWSTR pszOldPath,
            /* [string][in] */ LPCWSTR pszNewPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SyncBegin( 
            /* [in] */ REFGUID rSyncId) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SyncEnd( 
            /* [in] */ REFGUID rSyncId,
            /* [in] */ HRESULT hrResult) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SyncFileResult( 
            /* [in] */ REFGUID rSyncId,
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrResult) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE DataLost( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Ping( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CscuiInitialize( 
            /* [in] */ DWORD dwFlags) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CscuiNetUp( 
            /* [string][in] */ LPCWSTR pszPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CscuiNetDown( 
            /* [string][in] */ LPCWSTR pszPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CscuiCacheCorrupted( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesEvents * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *CacheMoved )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszOldPath,
            /* [string][in] */ LPCWSTR pszNewPath);
        
        HRESULT ( STDMETHODCALLTYPE *Enabled )( 
            IOfflineFilesEvents * This,
            /* [in] */ BOOL bEnabled);
        
        HRESULT ( STDMETHODCALLTYPE *EncryptionChanged )( 
            IOfflineFilesEvents * This,
            /* [in] */ BOOL bWasEncrypted,
            /* [in] */ BOOL bWasPartial,
            /* [in] */ BOOL bIsEncrypted,
            /* [in] */ BOOL bIsPartial);
        
        HRESULT ( STDMETHODCALLTYPE *FileAvailableOffline )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bDirectory,
            /* [in] */ BOOL bAvailableOffline);
        
        HRESULT ( STDMETHODCALLTYPE *FileDeleted )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszPath);
        
        HRESULT ( STDMETHODCALLTYPE *FilePinned )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bDirectory);
        
        HRESULT ( STDMETHODCALLTYPE *FileRollback )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszPath);
        
        HRESULT ( STDMETHODCALLTYPE *FileUnpinned )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bDirectory,
            /* [in] */ BOOL bLocalCopyDeleted);
        
        HRESULT ( STDMETHODCALLTYPE *PathConnected )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bConnected);
        
        HRESULT ( STDMETHODCALLTYPE *PathRenamed )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszOldPath,
            /* [string][in] */ LPCWSTR pszNewPath);
        
        HRESULT ( STDMETHODCALLTYPE *SyncBegin )( 
            IOfflineFilesEvents * This,
            /* [in] */ REFGUID rSyncId);
        
        HRESULT ( STDMETHODCALLTYPE *SyncEnd )( 
            IOfflineFilesEvents * This,
            /* [in] */ REFGUID rSyncId,
            /* [in] */ HRESULT hrResult);
        
        HRESULT ( STDMETHODCALLTYPE *SyncFileResult )( 
            IOfflineFilesEvents * This,
            /* [in] */ REFGUID rSyncId,
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrResult);
        
        HRESULT ( STDMETHODCALLTYPE *DataLost )( 
            IOfflineFilesEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *Ping )( 
            IOfflineFilesEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *CscuiInitialize )( 
            IOfflineFilesEvents * This,
            /* [in] */ DWORD dwFlags);
        
        HRESULT ( STDMETHODCALLTYPE *CscuiNetUp )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszPath);
        
        HRESULT ( STDMETHODCALLTYPE *CscuiNetDown )( 
            IOfflineFilesEvents * This,
            /* [string][in] */ LPCWSTR pszPath);
        
        HRESULT ( STDMETHODCALLTYPE *CscuiCacheCorrupted )( 
            IOfflineFilesEvents * This);
        
        END_INTERFACE
    } IOfflineFilesEventsVtbl;

    interface IOfflineFilesEvents
    {
        CONST_VTBL struct IOfflineFilesEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesEvents_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesEvents_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesEvents_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesEvents_CacheMoved(This,pszOldPath,pszNewPath)	\
    (This)->lpVtbl -> CacheMoved(This,pszOldPath,pszNewPath)

#define IOfflineFilesEvents_Enabled(This,bEnabled)	\
    (This)->lpVtbl -> Enabled(This,bEnabled)

#define IOfflineFilesEvents_EncryptionChanged(This,bWasEncrypted,bWasPartial,bIsEncrypted,bIsPartial)	\
    (This)->lpVtbl -> EncryptionChanged(This,bWasEncrypted,bWasPartial,bIsEncrypted,bIsPartial)

#define IOfflineFilesEvents_FileAvailableOffline(This,pszPath,bDirectory,bAvailableOffline)	\
    (This)->lpVtbl -> FileAvailableOffline(This,pszPath,bDirectory,bAvailableOffline)

#define IOfflineFilesEvents_FileDeleted(This,pszPath)	\
    (This)->lpVtbl -> FileDeleted(This,pszPath)

#define IOfflineFilesEvents_FilePinned(This,pszPath,bDirectory)	\
    (This)->lpVtbl -> FilePinned(This,pszPath,bDirectory)

#define IOfflineFilesEvents_FileRollback(This,pszPath)	\
    (This)->lpVtbl -> FileRollback(This,pszPath)

#define IOfflineFilesEvents_FileUnpinned(This,pszPath,bDirectory,bLocalCopyDeleted)	\
    (This)->lpVtbl -> FileUnpinned(This,pszPath,bDirectory,bLocalCopyDeleted)

#define IOfflineFilesEvents_PathConnected(This,pszPath,bConnected)	\
    (This)->lpVtbl -> PathConnected(This,pszPath,bConnected)

#define IOfflineFilesEvents_PathRenamed(This,pszOldPath,pszNewPath)	\
    (This)->lpVtbl -> PathRenamed(This,pszOldPath,pszNewPath)

#define IOfflineFilesEvents_SyncBegin(This,rSyncId)	\
    (This)->lpVtbl -> SyncBegin(This,rSyncId)

#define IOfflineFilesEvents_SyncEnd(This,rSyncId,hrResult)	\
    (This)->lpVtbl -> SyncEnd(This,rSyncId,hrResult)

#define IOfflineFilesEvents_SyncFileResult(This,rSyncId,pszFile,hrResult)	\
    (This)->lpVtbl -> SyncFileResult(This,rSyncId,pszFile,hrResult)

#define IOfflineFilesEvents_DataLost(This)	\
    (This)->lpVtbl -> DataLost(This)

#define IOfflineFilesEvents_Ping(This)	\
    (This)->lpVtbl -> Ping(This)

#define IOfflineFilesEvents_CscuiInitialize(This,dwFlags)	\
    (This)->lpVtbl -> CscuiInitialize(This,dwFlags)

#define IOfflineFilesEvents_CscuiNetUp(This,pszPath)	\
    (This)->lpVtbl -> CscuiNetUp(This,pszPath)

#define IOfflineFilesEvents_CscuiNetDown(This,pszPath)	\
    (This)->lpVtbl -> CscuiNetDown(This,pszPath)

#define IOfflineFilesEvents_CscuiCacheCorrupted(This)	\
    (This)->lpVtbl -> CscuiCacheCorrupted(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_CacheMoved_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszOldPath,
    /* [string][in] */ LPCWSTR pszNewPath);


void __RPC_STUB IOfflineFilesEvents_CacheMoved_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_Enabled_Proxy( 
    IOfflineFilesEvents * This,
    /* [in] */ BOOL bEnabled);


void __RPC_STUB IOfflineFilesEvents_Enabled_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_EncryptionChanged_Proxy( 
    IOfflineFilesEvents * This,
    /* [in] */ BOOL bWasEncrypted,
    /* [in] */ BOOL bWasPartial,
    /* [in] */ BOOL bIsEncrypted,
    /* [in] */ BOOL bIsPartial);


void __RPC_STUB IOfflineFilesEvents_EncryptionChanged_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_FileAvailableOffline_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [in] */ BOOL bDirectory,
    /* [in] */ BOOL bAvailableOffline);


void __RPC_STUB IOfflineFilesEvents_FileAvailableOffline_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_FileDeleted_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszPath);


void __RPC_STUB IOfflineFilesEvents_FileDeleted_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_FilePinned_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [in] */ BOOL bDirectory);


void __RPC_STUB IOfflineFilesEvents_FilePinned_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_FileRollback_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszPath);


void __RPC_STUB IOfflineFilesEvents_FileRollback_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_FileUnpinned_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [in] */ BOOL bDirectory,
    /* [in] */ BOOL bLocalCopyDeleted);


void __RPC_STUB IOfflineFilesEvents_FileUnpinned_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_PathConnected_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [in] */ BOOL bConnected);


void __RPC_STUB IOfflineFilesEvents_PathConnected_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_PathRenamed_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszOldPath,
    /* [string][in] */ LPCWSTR pszNewPath);


void __RPC_STUB IOfflineFilesEvents_PathRenamed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_SyncBegin_Proxy( 
    IOfflineFilesEvents * This,
    /* [in] */ REFGUID rSyncId);


void __RPC_STUB IOfflineFilesEvents_SyncBegin_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_SyncEnd_Proxy( 
    IOfflineFilesEvents * This,
    /* [in] */ REFGUID rSyncId,
    /* [in] */ HRESULT hrResult);


void __RPC_STUB IOfflineFilesEvents_SyncEnd_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_SyncFileResult_Proxy( 
    IOfflineFilesEvents * This,
    /* [in] */ REFGUID rSyncId,
    /* [string][in] */ LPCWSTR pszFile,
    /* [in] */ HRESULT hrResult);


void __RPC_STUB IOfflineFilesEvents_SyncFileResult_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_DataLost_Proxy( 
    IOfflineFilesEvents * This);


void __RPC_STUB IOfflineFilesEvents_DataLost_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_Ping_Proxy( 
    IOfflineFilesEvents * This);


void __RPC_STUB IOfflineFilesEvents_Ping_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_CscuiInitialize_Proxy( 
    IOfflineFilesEvents * This,
    /* [in] */ DWORD dwFlags);


void __RPC_STUB IOfflineFilesEvents_CscuiInitialize_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_CscuiNetUp_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszPath);


void __RPC_STUB IOfflineFilesEvents_CscuiNetUp_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_CscuiNetDown_Proxy( 
    IOfflineFilesEvents * This,
    /* [string][in] */ LPCWSTR pszPath);


void __RPC_STUB IOfflineFilesEvents_CscuiNetDown_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesEvents_CscuiCacheCorrupted_Proxy( 
    IOfflineFilesEvents * This);


void __RPC_STUB IOfflineFilesEvents_CscuiCacheCorrupted_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesEvents_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0261 */
/* [local] */ 

//
// IOfflineFilesProgress interface.
//
// The base interface for all progress interfaces.
// Provides the basic "begin" and "end" notifications that are common
// to all progress interfaces.
//
// Method: Begin
//
// Called when the operation begins.
//
// Method: End
//
// Called when the operation completes.
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0261_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0261_v0_0_s_ifspec;

#ifndef __IOfflineFilesProgress_INTERFACE_DEFINED__
#define __IOfflineFilesProgress_INTERFACE_DEFINED__

/* interface IOfflineFilesProgress */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesProgress;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FAD63237-C55B-4911-9850-BCF96D4C979E")
    IOfflineFilesProgress : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Begin( 
            /* [out] */ BOOL *pbAbort) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE End( 
            /* [in] */ HRESULT hrResult) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesProgressVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesProgress * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesProgress * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesProgress * This);
        
        HRESULT ( STDMETHODCALLTYPE *Begin )( 
            IOfflineFilesProgress * This,
            /* [out] */ BOOL *pbAbort);
        
        HRESULT ( STDMETHODCALLTYPE *End )( 
            IOfflineFilesProgress * This,
            /* [in] */ HRESULT hrResult);
        
        END_INTERFACE
    } IOfflineFilesProgressVtbl;

    interface IOfflineFilesProgress
    {
        CONST_VTBL struct IOfflineFilesProgressVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesProgress_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesProgress_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesProgress_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesProgress_Begin(This,pbAbort)	\
    (This)->lpVtbl -> Begin(This,pbAbort)

#define IOfflineFilesProgress_End(This,hrResult)	\
    (This)->lpVtbl -> End(This,hrResult)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesProgress_Begin_Proxy( 
    IOfflineFilesProgress * This,
    /* [out] */ BOOL *pbAbort);


void __RPC_STUB IOfflineFilesProgress_Begin_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesProgress_End_Proxy( 
    IOfflineFilesProgress * This,
    /* [in] */ HRESULT hrResult);


void __RPC_STUB IOfflineFilesProgress_End_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesProgress_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0262 */
/* [local] */ 

//
// IOfflineFilesSimpleProgress interface.
//
// Implement this interface when you wish to receive progress
// notifications from the following methods:
//
//    IOfflineFilesCache::Encrypt
//    IOfflineFilesCache::Delete
//    IOfflineFilesCache::Pin
//    IOfflineFilesCache::Unpin
//
// The progress interface used by many of the Offline Files methods that
// accept progress implementations.  Along with the "begin" and "end" 
// methods it inherits from IOfflineFilesProgress, it provides an 
// "item result" method to report success/failure for an operation
// on a particular file or directory in the Offline Files cache.  The client
// can choose to continue, retry, or abort the operation.
//
// Method: ItemBegin
//
// Called when one item (file or directory) is beginning to be
// processed by the associated operation.
//
// Method: ItemResult
//
// Called when one item (file or directory) has been processed
// by the associated operation.
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0262_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0262_v0_0_s_ifspec;

#ifndef __IOfflineFilesSimpleProgress_INTERFACE_DEFINED__
#define __IOfflineFilesSimpleProgress_INTERFACE_DEFINED__

/* interface IOfflineFilesSimpleProgress */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesSimpleProgress;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("C34F7F9B-C43D-4f9d-A776-C0EB6DE5D401")
    IOfflineFilesSimpleProgress : public IOfflineFilesProgress
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE ItemBegin( 
            /* [string][in] */ LPCWSTR pszFile,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE ItemResult( 
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrResult,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesSimpleProgressVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesSimpleProgress * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesSimpleProgress * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesSimpleProgress * This);
        
        HRESULT ( STDMETHODCALLTYPE *Begin )( 
            IOfflineFilesSimpleProgress * This,
            /* [out] */ BOOL *pbAbort);
        
        HRESULT ( STDMETHODCALLTYPE *End )( 
            IOfflineFilesSimpleProgress * This,
            /* [in] */ HRESULT hrResult);
        
        HRESULT ( STDMETHODCALLTYPE *ItemBegin )( 
            IOfflineFilesSimpleProgress * This,
            /* [string][in] */ LPCWSTR pszFile,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);
        
        HRESULT ( STDMETHODCALLTYPE *ItemResult )( 
            IOfflineFilesSimpleProgress * This,
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrResult,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);
        
        END_INTERFACE
    } IOfflineFilesSimpleProgressVtbl;

    interface IOfflineFilesSimpleProgress
    {
        CONST_VTBL struct IOfflineFilesSimpleProgressVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesSimpleProgress_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesSimpleProgress_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesSimpleProgress_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesSimpleProgress_Begin(This,pbAbort)	\
    (This)->lpVtbl -> Begin(This,pbAbort)

#define IOfflineFilesSimpleProgress_End(This,hrResult)	\
    (This)->lpVtbl -> End(This,hrResult)


#define IOfflineFilesSimpleProgress_ItemBegin(This,pszFile,pResponse)	\
    (This)->lpVtbl -> ItemBegin(This,pszFile,pResponse)

#define IOfflineFilesSimpleProgress_ItemResult(This,pszFile,hrResult,pResponse)	\
    (This)->lpVtbl -> ItemResult(This,pszFile,hrResult,pResponse)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesSimpleProgress_ItemBegin_Proxy( 
    IOfflineFilesSimpleProgress * This,
    /* [string][in] */ LPCWSTR pszFile,
    /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);


void __RPC_STUB IOfflineFilesSimpleProgress_ItemBegin_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSimpleProgress_ItemResult_Proxy( 
    IOfflineFilesSimpleProgress * This,
    /* [string][in] */ LPCWSTR pszFile,
    /* [in] */ HRESULT hrResult,
    /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);


void __RPC_STUB IOfflineFilesSimpleProgress_ItemResult_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesSimpleProgress_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0263 */
/* [local] */ 

//
// IOfflineFilesSyncProgress interface
//
// Implement this interface when you wish to receive progress
// notifications from the following method:
//
//    IOfflineFilesCache::Synchronize
//
// This is a specialization of IOfflineFilesSimpleProgress.
// It adds methods for the searching for and pinning of new
// files inside pinned folders during synchronization.
//
// Method: QueryCancel
//
// Called periodically to determine if the sync operation should
// be cancelled.
//
// Method: NewFileScanBegin
//
// Called when the service begins scanning the network file system
// for new files existing inside pinned folders.
//
// Method: NewFileScanEnd
//
// Called when the service has finished scanning the network file system
// for new files existing inside pinned folders.
//
// Method: PinNewFileResult
//
// Called when a new file found inside a pinned folder has been pinned
// itself or when that pin operation has failed.
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0263_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0263_v0_0_s_ifspec;

#ifndef __IOfflineFilesSyncProgress_INTERFACE_DEFINED__
#define __IOfflineFilesSyncProgress_INTERFACE_DEFINED__

/* interface IOfflineFilesSyncProgress */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesSyncProgress;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("734C6091-A6D2-4908-9B2F-09C880DDAD78")
    IOfflineFilesSyncProgress : public IOfflineFilesSimpleProgress
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE QueryCancel( 
            /* [out] */ BOOL *pbCancel) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE NewFileScanBegin( 
            /* [out] */ BOOL *pbAbort) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE NewFileScanEnd( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE PinNewFileResult( 
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrPin,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesSyncProgressVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesSyncProgress * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesSyncProgress * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesSyncProgress * This);
        
        HRESULT ( STDMETHODCALLTYPE *Begin )( 
            IOfflineFilesSyncProgress * This,
            /* [out] */ BOOL *pbAbort);
        
        HRESULT ( STDMETHODCALLTYPE *End )( 
            IOfflineFilesSyncProgress * This,
            /* [in] */ HRESULT hrResult);
        
        HRESULT ( STDMETHODCALLTYPE *ItemBegin )( 
            IOfflineFilesSyncProgress * This,
            /* [string][in] */ LPCWSTR pszFile,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);
        
        HRESULT ( STDMETHODCALLTYPE *ItemResult )( 
            IOfflineFilesSyncProgress * This,
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrResult,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);
        
        HRESULT ( STDMETHODCALLTYPE *QueryCancel )( 
            IOfflineFilesSyncProgress * This,
            /* [out] */ BOOL *pbCancel);
        
        HRESULT ( STDMETHODCALLTYPE *NewFileScanBegin )( 
            IOfflineFilesSyncProgress * This,
            /* [out] */ BOOL *pbAbort);
        
        HRESULT ( STDMETHODCALLTYPE *NewFileScanEnd )( 
            IOfflineFilesSyncProgress * This);
        
        HRESULT ( STDMETHODCALLTYPE *PinNewFileResult )( 
            IOfflineFilesSyncProgress * This,
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrPin,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);
        
        END_INTERFACE
    } IOfflineFilesSyncProgressVtbl;

    interface IOfflineFilesSyncProgress
    {
        CONST_VTBL struct IOfflineFilesSyncProgressVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesSyncProgress_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesSyncProgress_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesSyncProgress_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesSyncProgress_Begin(This,pbAbort)	\
    (This)->lpVtbl -> Begin(This,pbAbort)

#define IOfflineFilesSyncProgress_End(This,hrResult)	\
    (This)->lpVtbl -> End(This,hrResult)


#define IOfflineFilesSyncProgress_ItemBegin(This,pszFile,pResponse)	\
    (This)->lpVtbl -> ItemBegin(This,pszFile,pResponse)

#define IOfflineFilesSyncProgress_ItemResult(This,pszFile,hrResult,pResponse)	\
    (This)->lpVtbl -> ItemResult(This,pszFile,hrResult,pResponse)


#define IOfflineFilesSyncProgress_QueryCancel(This,pbCancel)	\
    (This)->lpVtbl -> QueryCancel(This,pbCancel)

#define IOfflineFilesSyncProgress_NewFileScanBegin(This,pbAbort)	\
    (This)->lpVtbl -> NewFileScanBegin(This,pbAbort)

#define IOfflineFilesSyncProgress_NewFileScanEnd(This)	\
    (This)->lpVtbl -> NewFileScanEnd(This)

#define IOfflineFilesSyncProgress_PinNewFileResult(This,pszFile,hrPin,pResponse)	\
    (This)->lpVtbl -> PinNewFileResult(This,pszFile,hrPin,pResponse)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesSyncProgress_QueryCancel_Proxy( 
    IOfflineFilesSyncProgress * This,
    /* [out] */ BOOL *pbCancel);


void __RPC_STUB IOfflineFilesSyncProgress_QueryCancel_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSyncProgress_NewFileScanBegin_Proxy( 
    IOfflineFilesSyncProgress * This,
    /* [out] */ BOOL *pbAbort);


void __RPC_STUB IOfflineFilesSyncProgress_NewFileScanBegin_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSyncProgress_NewFileScanEnd_Proxy( 
    IOfflineFilesSyncProgress * This);


void __RPC_STUB IOfflineFilesSyncProgress_NewFileScanEnd_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSyncProgress_PinNewFileResult_Proxy( 
    IOfflineFilesSyncProgress * This,
    /* [string][in] */ LPCWSTR pszFile,
    /* [in] */ HRESULT hrPin,
    /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);


void __RPC_STUB IOfflineFilesSyncProgress_PinNewFileResult_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesSyncProgress_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0264 */
/* [local] */ 

//
// IOfflineFilesCacheMoveProgress interface
//
// Implement this interface when you wish to receive progress
// notifications from the following method:
//
//    IOfflineFilesCache::MoveCache
//
// This is a specialization of IOfflineFilesProgress.
// It adds methods for reporting detailed progress while
// moving the CSC cache.  Since ItemResult() is not applicable
// we derive from IOfflineFilesProgress rather than IOfflineFilesSimpleProgress.
//
// Method: CopyFileBegin
//
// Called when a file is about to be copied to the new location.
//
// Method: CopyFileProgress
//
// Called as a file is being copied to the new location.  This notification
// includes a progress indication for the current file as well as for the entire
// cache move operation.  The progress indications are expressed as a 0-100 integer
// percentage.
//
// Method: CopyFileEnd
//
// Called when a file has been copied or when an attempt to copy the file has failed.
//
// Method: DeleteFileProgress
//
// Once the entire copy operation has completed successfully, the original files are 
// removed from the original location.  This method is called for each file removed.
// To report 0-100 percent progress of the delete operation, calculate a cummulative
// count of files copied (via the implementation of CopyFileEnd).  Use that count
// in the DeleteFileProgress implementation to compute the percent-complete for the
// delete operation.
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0264_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0264_v0_0_s_ifspec;

#ifndef __IOfflineFilesCacheMoveProgress_INTERFACE_DEFINED__
#define __IOfflineFilesCacheMoveProgress_INTERFACE_DEFINED__

/* interface IOfflineFilesCacheMoveProgress */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesCacheMoveProgress;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("7DB89040-B032-4a28-B1CB-A04C45AFB374")
    IOfflineFilesCacheMoveProgress : public IOfflineFilesProgress
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE CopyFileBegin( 
            /* [string][in] */ LPCWSTR pszFileSource,
            /* [string][in] */ LPCWSTR pszFileDest,
            /* [out] */ BOOL *pbAbort) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CopyFileProgress( 
            /* [in] */ LONG pctCompleteFile,
            /* [in] */ LONG pctCompleteTotal,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CopyFileEnd( 
            /* [string][in] */ LPCWSTR pszFileSource,
            /* [string][in] */ LPCWSTR pszFileDest,
            /* [in] */ HRESULT hrCopy,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE DeleteFileProgress( 
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrDelete,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesCacheMoveProgressVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesCacheMoveProgress * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesCacheMoveProgress * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesCacheMoveProgress * This);
        
        HRESULT ( STDMETHODCALLTYPE *Begin )( 
            IOfflineFilesCacheMoveProgress * This,
            /* [out] */ BOOL *pbAbort);
        
        HRESULT ( STDMETHODCALLTYPE *End )( 
            IOfflineFilesCacheMoveProgress * This,
            /* [in] */ HRESULT hrResult);
        
        HRESULT ( STDMETHODCALLTYPE *CopyFileBegin )( 
            IOfflineFilesCacheMoveProgress * This,
            /* [string][in] */ LPCWSTR pszFileSource,
            /* [string][in] */ LPCWSTR pszFileDest,
            /* [out] */ BOOL *pbAbort);
        
        HRESULT ( STDMETHODCALLTYPE *CopyFileProgress )( 
            IOfflineFilesCacheMoveProgress * This,
            /* [in] */ LONG pctCompleteFile,
            /* [in] */ LONG pctCompleteTotal,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);
        
        HRESULT ( STDMETHODCALLTYPE *CopyFileEnd )( 
            IOfflineFilesCacheMoveProgress * This,
            /* [string][in] */ LPCWSTR pszFileSource,
            /* [string][in] */ LPCWSTR pszFileDest,
            /* [in] */ HRESULT hrCopy,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);
        
        HRESULT ( STDMETHODCALLTYPE *DeleteFileProgress )( 
            IOfflineFilesCacheMoveProgress * This,
            /* [string][in] */ LPCWSTR pszFile,
            /* [in] */ HRESULT hrDelete,
            /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);
        
        END_INTERFACE
    } IOfflineFilesCacheMoveProgressVtbl;

    interface IOfflineFilesCacheMoveProgress
    {
        CONST_VTBL struct IOfflineFilesCacheMoveProgressVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesCacheMoveProgress_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesCacheMoveProgress_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesCacheMoveProgress_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesCacheMoveProgress_Begin(This,pbAbort)	\
    (This)->lpVtbl -> Begin(This,pbAbort)

#define IOfflineFilesCacheMoveProgress_End(This,hrResult)	\
    (This)->lpVtbl -> End(This,hrResult)


#define IOfflineFilesCacheMoveProgress_CopyFileBegin(This,pszFileSource,pszFileDest,pbAbort)	\
    (This)->lpVtbl -> CopyFileBegin(This,pszFileSource,pszFileDest,pbAbort)

#define IOfflineFilesCacheMoveProgress_CopyFileProgress(This,pctCompleteFile,pctCompleteTotal,pResponse)	\
    (This)->lpVtbl -> CopyFileProgress(This,pctCompleteFile,pctCompleteTotal,pResponse)

#define IOfflineFilesCacheMoveProgress_CopyFileEnd(This,pszFileSource,pszFileDest,hrCopy,pResponse)	\
    (This)->lpVtbl -> CopyFileEnd(This,pszFileSource,pszFileDest,hrCopy,pResponse)

#define IOfflineFilesCacheMoveProgress_DeleteFileProgress(This,pszFile,hrDelete,pResponse)	\
    (This)->lpVtbl -> DeleteFileProgress(This,pszFile,hrDelete,pResponse)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesCacheMoveProgress_CopyFileBegin_Proxy( 
    IOfflineFilesCacheMoveProgress * This,
    /* [string][in] */ LPCWSTR pszFileSource,
    /* [string][in] */ LPCWSTR pszFileDest,
    /* [out] */ BOOL *pbAbort);


void __RPC_STUB IOfflineFilesCacheMoveProgress_CopyFileBegin_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCacheMoveProgress_CopyFileProgress_Proxy( 
    IOfflineFilesCacheMoveProgress * This,
    /* [in] */ LONG pctCompleteFile,
    /* [in] */ LONG pctCompleteTotal,
    /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);


void __RPC_STUB IOfflineFilesCacheMoveProgress_CopyFileProgress_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCacheMoveProgress_CopyFileEnd_Proxy( 
    IOfflineFilesCacheMoveProgress * This,
    /* [string][in] */ LPCWSTR pszFileSource,
    /* [string][in] */ LPCWSTR pszFileDest,
    /* [in] */ HRESULT hrCopy,
    /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);


void __RPC_STUB IOfflineFilesCacheMoveProgress_CopyFileEnd_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCacheMoveProgress_DeleteFileProgress_Proxy( 
    IOfflineFilesCacheMoveProgress * This,
    /* [string][in] */ LPCWSTR pszFile,
    /* [in] */ HRESULT hrDelete,
    /* [out] */ OFFLINEFILES_OP_RESPONSE *pResponse);


void __RPC_STUB IOfflineFilesCacheMoveProgress_DeleteFileProgress_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesCacheMoveProgress_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0265 */
/* [local] */ 

typedef 
enum tagOFFLINEFILES_SYNC_STATE
    {	OFFLINEFILES_SYNC_STATE_NoConflict	= 0,
	OFFLINEFILES_SYNC_STATE_FileCreatedOnClient_NoServerCopy	= OFFLINEFILES_SYNC_STATE_NoConflict + 1,
	OFFLINEFILES_SYNC_STATE_DirCreatedOnClient_NoServerCopy	= OFFLINEFILES_SYNC_STATE_FileCreatedOnClient_NoServerCopy + 1,
	OFFLINEFILES_SYNC_STATE_FileSparseOnClient	= OFFLINEFILES_SYNC_STATE_DirCreatedOnClient_NoServerCopy + 1,
	OFFLINEFILES_SYNC_STATE_FileRenamedOnServer	= OFFLINEFILES_SYNC_STATE_FileSparseOnClient + 1,
	OFFLINEFILES_SYNC_STATE_DirRenamedOnServer	= OFFLINEFILES_SYNC_STATE_FileRenamedOnServer + 1,
	OFFLINEFILES_SYNC_STATE_FileDeletedOnServer	= OFFLINEFILES_SYNC_STATE_DirRenamedOnServer + 1,
	OFFLINEFILES_SYNC_STATE_DirDeletedOnServer	= OFFLINEFILES_SYNC_STATE_FileDeletedOnServer + 1,
	OFFLINEFILES_SYNC_STATE_FileCreatedOnClient_FileOnServer	= OFFLINEFILES_SYNC_STATE_DirDeletedOnServer + 1,
	OFFLINEFILES_SYNC_STATE_DirCreatedOnClient_DirOnServer	= OFFLINEFILES_SYNC_STATE_FileCreatedOnClient_FileOnServer + 1,
	OFFLINEFILES_SYNC_STATE_FileCreatedOnClient_DirOnServer	= OFFLINEFILES_SYNC_STATE_DirCreatedOnClient_DirOnServer + 1,
	OFFLINEFILES_SYNC_STATE_DirCreatedOnClient_FileOnServer	= OFFLINEFILES_SYNC_STATE_FileCreatedOnClient_DirOnServer + 1,
	OFFLINEFILES_SYNC_STATE_FileRenamedOnClient	= OFFLINEFILES_SYNC_STATE_DirCreatedOnClient_FileOnServer + 1,
	OFFLINEFILES_SYNC_STATE_DirRenamedOnClient	= OFFLINEFILES_SYNC_STATE_FileRenamedOnClient + 1,
	OFFLINEFILES_SYNC_STATE_FileDeletedOnClient	= OFFLINEFILES_SYNC_STATE_DirRenamedOnClient + 1,
	OFFLINEFILES_SYNC_STATE_DirDeletedOnClient	= OFFLINEFILES_SYNC_STATE_FileDeletedOnClient + 1,
	OFFLINEFILES_SYNC_STATE_FileChangedOnClient	= OFFLINEFILES_SYNC_STATE_DirDeletedOnClient + 1,
	OFFLINEFILES_SYNC_STATE_FileChangedOnServer	= OFFLINEFILES_SYNC_STATE_FileChangedOnClient + 1,
	OFFLINEFILES_SYNC_STATE_FileChangedOnClient_ChangedOnServer	= OFFLINEFILES_SYNC_STATE_FileChangedOnServer + 1,
	OFFLINEFILES_SYNC_STATE_FileChangedOnClient_DeletedOnServer	= OFFLINEFILES_SYNC_STATE_FileChangedOnClient_ChangedOnServer + 1,
	OFFLINEFILES_SYNC_STATE_FileDeleteOnClient_ChangedOnServer	= OFFLINEFILES_SYNC_STATE_FileChangedOnClient_DeletedOnServer + 1,
	OFFLINEFILES_SYNC_STATE_NUMSTATES	= OFFLINEFILES_SYNC_STATE_FileDeleteOnClient_ChangedOnServer + 1
    } 	OFFLINEFILES_SYNC_STATE;

#define	OFFLINEFILES_CHANGES_NONE	( 0 )

#define	OFFLINEFILES_CHANGES_LOCAL_DATA	( 0x1 )

#define	OFFLINEFILES_CHANGES_LOCAL_ATTRIBUTES	( 0x2 )

#define	OFFLINEFILES_CHANGES_LOCAL_TIME	( 0x4 )

#define	OFFLINEFILES_CHANGES_REMOTE_SIZE	( 0x8 )

#define	OFFLINEFILES_CHANGES_REMOTE_ATTRIBUTES	( 0x10 )

#define	OFFLINEFILES_CHANGES_REMOTE_TIME	( 0x20 )

typedef 
enum tagOFFLINEFILES_SYNC_CONFLICT_RESOLVE
    {	OFFLINEFILES_SYNC_CONFLICT_RESOLVE_NONE	= 0,
	OFFLINEFILES_SYNC_CONFLICT_RESOLVE_KEEPLOCAL	= OFFLINEFILES_SYNC_CONFLICT_RESOLVE_NONE + 1,
	OFFLINEFILES_SYNC_CONFLICT_RESOLVE_KEEPREMOTE	= OFFLINEFILES_SYNC_CONFLICT_RESOLVE_KEEPLOCAL + 1,
	OFFLINEFILES_SYNC_CONFLICT_RESOLVE_KEEPALLCHANGES	= OFFLINEFILES_SYNC_CONFLICT_RESOLVE_KEEPREMOTE + 1,
	OFFLINEFILES_SYNC_CONFLICT_RESOLVE_KEEPLATEST	= OFFLINEFILES_SYNC_CONFLICT_RESOLVE_KEEPALLCHANGES + 1,
	OFFLINEFILES_SYNC_CONFLICT_RESOLVE_LATER	= OFFLINEFILES_SYNC_CONFLICT_RESOLVE_KEEPLATEST + 1,
	OFFLINEFILES_SYNC_CONFLICT_RESOLVE_NUMCODES	= OFFLINEFILES_SYNC_CONFLICT_RESOLVE_LATER + 1
    } 	OFFLINEFILES_SYNC_CONFLICT_RESOLVE;



extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0265_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0265_v0_0_s_ifspec;

#ifndef __IOfflineFilesSyncConflictHandler_INTERFACE_DEFINED__
#define __IOfflineFilesSyncConflictHandler_INTERFACE_DEFINED__

/* interface IOfflineFilesSyncConflictHandler */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesSyncConflictHandler;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B6DD5092-C65C-46b6-97B8-FADD08E7E1BE")
    IOfflineFilesSyncConflictHandler : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE ResolveConflict( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ OFFLINEFILES_SYNC_STATE state,
            /* [in] */ DWORD fChangeDetails,
            /* [out] */ OFFLINEFILES_SYNC_CONFLICT_RESOLVE *pConflictResolution) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesSyncConflictHandlerVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesSyncConflictHandler * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesSyncConflictHandler * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesSyncConflictHandler * This);
        
        HRESULT ( STDMETHODCALLTYPE *ResolveConflict )( 
            IOfflineFilesSyncConflictHandler * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ OFFLINEFILES_SYNC_STATE state,
            /* [in] */ DWORD fChangeDetails,
            /* [out] */ OFFLINEFILES_SYNC_CONFLICT_RESOLVE *pConflictResolution);
        
        END_INTERFACE
    } IOfflineFilesSyncConflictHandlerVtbl;

    interface IOfflineFilesSyncConflictHandler
    {
        CONST_VTBL struct IOfflineFilesSyncConflictHandlerVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesSyncConflictHandler_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesSyncConflictHandler_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesSyncConflictHandler_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesSyncConflictHandler_ResolveConflict(This,pszPath,state,fChangeDetails,pConflictResolution)	\
    (This)->lpVtbl -> ResolveConflict(This,pszPath,state,fChangeDetails,pConflictResolution)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesSyncConflictHandler_ResolveConflict_Proxy( 
    IOfflineFilesSyncConflictHandler * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [in] */ OFFLINEFILES_SYNC_STATE state,
    /* [in] */ DWORD fChangeDetails,
    /* [out] */ OFFLINEFILES_SYNC_CONFLICT_RESOLVE *pConflictResolution);


void __RPC_STUB IOfflineFilesSyncConflictHandler_ResolveConflict_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesSyncConflictHandler_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0266 */
/* [local] */ 


//
// IOfflineFile interface.
//
// Implemented by Windows.
//
// Represents a single file or directory in the Offline Files cache.
// If the item is a directory, its children are obtained through
// an enumerator created by the EnumFiles method.  Using 
// IOfflineFilesCache::EnumShareRoots, IOfflineFile,
// and IEnumOfflineFiles, a client is able to enumerate the entire
// Offline Files cache.
//
// Method: GetPath
//
// Retrieves the fully-qualified UNC path for the file
// or directory.
//
// Method: GetStatus
//
// Retrieves the current status information for the file 
// or directory
//
// Method: EnumFiles
//
// Creates an enumerator object for enumerating the immediate
// children of a directory entry.  If this method is called
// for a file object, an empty enumerator object is returned.
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0266_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0266_v0_0_s_ifspec;

#ifndef __IOfflineFile_INTERFACE_DEFINED__
#define __IOfflineFile_INTERFACE_DEFINED__

/* interface IOfflineFile */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFile;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("AE3D3247-2D8D-40ce-B76E-6C3124831B79")
    IOfflineFile : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetPath( 
            /* [string][out] */ LPWSTR *ppszPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetStatus( 
            /* [out] */ OFFLINEFILES_FILE_STATUS *pStatus) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetShareStatus( 
            /* [out] */ OFFLINEFILES_SHARE_STATUS *pStatus) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetCachingMode( 
            /* [out] */ OFFLINEFILES_CACHING_MODE *pMode) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE EnumFiles( 
            /* [out] */ IEnumOfflineFiles **ppenum) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetParent( 
            /* [out] */ IOfflineFile **ppParent) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE IsDirectory( 
            /* [out] */ BOOL *pbIsDirectory) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFileVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFile * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFile * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFile * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetPath )( 
            IOfflineFile * This,
            /* [string][out] */ LPWSTR *ppszPath);
        
        HRESULT ( STDMETHODCALLTYPE *GetStatus )( 
            IOfflineFile * This,
            /* [out] */ OFFLINEFILES_FILE_STATUS *pStatus);
        
        HRESULT ( STDMETHODCALLTYPE *GetShareStatus )( 
            IOfflineFile * This,
            /* [out] */ OFFLINEFILES_SHARE_STATUS *pStatus);
        
        HRESULT ( STDMETHODCALLTYPE *GetCachingMode )( 
            IOfflineFile * This,
            /* [out] */ OFFLINEFILES_CACHING_MODE *pMode);
        
        HRESULT ( STDMETHODCALLTYPE *EnumFiles )( 
            IOfflineFile * This,
            /* [out] */ IEnumOfflineFiles **ppenum);
        
        HRESULT ( STDMETHODCALLTYPE *GetParent )( 
            IOfflineFile * This,
            /* [out] */ IOfflineFile **ppParent);
        
        HRESULT ( STDMETHODCALLTYPE *IsDirectory )( 
            IOfflineFile * This,
            /* [out] */ BOOL *pbIsDirectory);
        
        END_INTERFACE
    } IOfflineFileVtbl;

    interface IOfflineFile
    {
        CONST_VTBL struct IOfflineFileVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFile_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFile_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFile_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFile_GetPath(This,ppszPath)	\
    (This)->lpVtbl -> GetPath(This,ppszPath)

#define IOfflineFile_GetStatus(This,pStatus)	\
    (This)->lpVtbl -> GetStatus(This,pStatus)

#define IOfflineFile_GetShareStatus(This,pStatus)	\
    (This)->lpVtbl -> GetShareStatus(This,pStatus)

#define IOfflineFile_GetCachingMode(This,pMode)	\
    (This)->lpVtbl -> GetCachingMode(This,pMode)

#define IOfflineFile_EnumFiles(This,ppenum)	\
    (This)->lpVtbl -> EnumFiles(This,ppenum)

#define IOfflineFile_GetParent(This,ppParent)	\
    (This)->lpVtbl -> GetParent(This,ppParent)

#define IOfflineFile_IsDirectory(This,pbIsDirectory)	\
    (This)->lpVtbl -> IsDirectory(This,pbIsDirectory)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFile_GetPath_Proxy( 
    IOfflineFile * This,
    /* [string][out] */ LPWSTR *ppszPath);


void __RPC_STUB IOfflineFile_GetPath_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFile_GetStatus_Proxy( 
    IOfflineFile * This,
    /* [out] */ OFFLINEFILES_FILE_STATUS *pStatus);


void __RPC_STUB IOfflineFile_GetStatus_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFile_GetShareStatus_Proxy( 
    IOfflineFile * This,
    /* [out] */ OFFLINEFILES_SHARE_STATUS *pStatus);


void __RPC_STUB IOfflineFile_GetShareStatus_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFile_GetCachingMode_Proxy( 
    IOfflineFile * This,
    /* [out] */ OFFLINEFILES_CACHING_MODE *pMode);


void __RPC_STUB IOfflineFile_GetCachingMode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFile_EnumFiles_Proxy( 
    IOfflineFile * This,
    /* [out] */ IEnumOfflineFiles **ppenum);


void __RPC_STUB IOfflineFile_EnumFiles_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFile_GetParent_Proxy( 
    IOfflineFile * This,
    /* [out] */ IOfflineFile **ppParent);


void __RPC_STUB IOfflineFile_GetParent_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFile_IsDirectory_Proxy( 
    IOfflineFile * This,
    /* [out] */ BOOL *pbIsDirectory);


void __RPC_STUB IOfflineFile_IsDirectory_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFile_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0267 */
/* [local] */ 

//
// IEnumOfflineFiles interface.
//
// Implemented by Windows.
//
// This interface is a standard COM enumerator for enumerating
// the immediate children of a directory in the Offline Files
// cache.
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0267_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0267_v0_0_s_ifspec;

#ifndef __IEnumOfflineFiles_INTERFACE_DEFINED__
#define __IEnumOfflineFiles_INTERFACE_DEFINED__

/* interface IEnumOfflineFiles */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_IEnumOfflineFiles;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("60BC9146-13FC-4110-A1E2-93F8D52F1AD9")
    IEnumOfflineFiles : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Next( 
            /* [in] */ ULONG celt,
            /* [length_is][size_is][out] */ IOfflineFile **rgelt,
            /* [out] */ ULONG *pceltFetched) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Skip( 
            /* [in] */ ULONG celt) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Reset( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Clone( 
            /* [out] */ IEnumOfflineFiles **ppenum) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IEnumOfflineFilesVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IEnumOfflineFiles * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IEnumOfflineFiles * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IEnumOfflineFiles * This);
        
        HRESULT ( STDMETHODCALLTYPE *Next )( 
            IEnumOfflineFiles * This,
            /* [in] */ ULONG celt,
            /* [length_is][size_is][out] */ IOfflineFile **rgelt,
            /* [out] */ ULONG *pceltFetched);
        
        HRESULT ( STDMETHODCALLTYPE *Skip )( 
            IEnumOfflineFiles * This,
            /* [in] */ ULONG celt);
        
        HRESULT ( STDMETHODCALLTYPE *Reset )( 
            IEnumOfflineFiles * This);
        
        HRESULT ( STDMETHODCALLTYPE *Clone )( 
            IEnumOfflineFiles * This,
            /* [out] */ IEnumOfflineFiles **ppenum);
        
        END_INTERFACE
    } IEnumOfflineFilesVtbl;

    interface IEnumOfflineFiles
    {
        CONST_VTBL struct IEnumOfflineFilesVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IEnumOfflineFiles_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IEnumOfflineFiles_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IEnumOfflineFiles_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IEnumOfflineFiles_Next(This,celt,rgelt,pceltFetched)	\
    (This)->lpVtbl -> Next(This,celt,rgelt,pceltFetched)

#define IEnumOfflineFiles_Skip(This,celt)	\
    (This)->lpVtbl -> Skip(This,celt)

#define IEnumOfflineFiles_Reset(This)	\
    (This)->lpVtbl -> Reset(This)

#define IEnumOfflineFiles_Clone(This,ppenum)	\
    (This)->lpVtbl -> Clone(This,ppenum)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IEnumOfflineFiles_Next_Proxy( 
    IEnumOfflineFiles * This,
    /* [in] */ ULONG celt,
    /* [length_is][size_is][out] */ IOfflineFile **rgelt,
    /* [out] */ ULONG *pceltFetched);


void __RPC_STUB IEnumOfflineFiles_Next_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumOfflineFiles_Skip_Proxy( 
    IEnumOfflineFiles * This,
    /* [in] */ ULONG celt);


void __RPC_STUB IEnumOfflineFiles_Skip_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumOfflineFiles_Reset_Proxy( 
    IEnumOfflineFiles * This);


void __RPC_STUB IEnumOfflineFiles_Reset_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumOfflineFiles_Clone_Proxy( 
    IEnumOfflineFiles * This,
    /* [out] */ IEnumOfflineFiles **ppenum);


void __RPC_STUB IEnumOfflineFiles_Clone_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IEnumOfflineFiles_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0268 */
/* [local] */ 

//
// "Scope" of a setting value.
//
// Settings are the "preferences" and "policies" that control the
// behavior of Offline Files.  "Preferences" are those values 
// expressed by the client either through the user interface or
// API functions.  "Policies" are those values expressed by
// system administrators through Group Policy.  A given setting
// may be expressed through preference, policy, or a combination
// of both.  Each type may be expressed as per-user, per-machine,
// or both per-user and per-machine depending on the type of setting.
// This is known as the "scope" of the setting.
//
// In cases where multiple values exist for a given setting, the system 
// selects a value based on the following list of precedence rules 
// ordered in decreasing priority.
//
//    1. Per-machine policy
//    2. Per-user policy
//    3. Per-machine preference
//    4. Per-user preference
//    5. System default
//
// OFFLINEFILES_SETTING_SCOPE_USER     - per user
// OFFLINEFILES_SETTING_SCOPE_COMPUTER - per machine
//
typedef 
enum tagOFFLINEFILES_SETTING_SCOPE
    {	OFFLINEFILES_SETTING_SCOPE_USER	= 0x1,
	OFFLINEFILES_SETTING_SCOPE_COMPUTER	= 0x2
    } 	OFFLINEFILES_SETTING_SCOPE;

//
// IOfflineFilesSetting interface
//
// Implemented by Windows.
//
// This interface represents a single setting that controls some aspect
// of Offline Files operation.
//
// To query or adjust a setting, first call IOfflineFilesCache::GetSettingObject
// to obtain the IOfflineFilesSetting interface for that setting.
// Through this interface you can query or adjust the value(s) associated with 
// that setting.  Note that the interface uses the VARIANT data type to represent
// the data associated with settings.  Some settings are represented with
// a single numeric value while others require a text value, multiple numeric
// values, or multiple text values.  By using the type VARIANT we can support
// these different data type requirements through a more simplistic interface.
//
// See the programming documentation on the Offline Files API for details
// on the settings supported and their respective value data formats.
//
// Method: GetPreference
//
// Call to obtain a current per-user or per-machine "preference"
// value for the setting.
//
// Method: GetPreferenceScope
//
// Call to obtain the per-user/per-machine scope of the preference.
//
// Method: SetPreference
//
// Call to modify a per-user or per-machine preference value.
//
// Method: DeletePreference
//
// Call to delete a per-user or per-machine preference value.
//
// Method: GetPolicy
//
// Call to obtain a current per-user or per-machine "policy"
// value for the setting.
//
// Method: GetPolicyScope
//
// Call to obtain the per-user/per-machine scope of the policy.
//
// Method: GetValue
//
// Call to obtain a resultant value for the setting.  This evaluates
// all available policy, preference, and default values associated with
// the setting to establish the resultant value returned.
//
// In cases where multiple values exist for a given setting, the system
// selects a value based on the following list of precedence rules 
// ordered in decreasing priority.
//
//    1. Per-machine policy
//    2. Per-user policy
//    3. Per-machine preference
//    4. Per-user preference
//    5. System default
//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0269_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0269_v0_0_s_ifspec;

#ifndef __IOfflineFilesSetting_INTERFACE_DEFINED__
#define __IOfflineFilesSetting_INTERFACE_DEFINED__

/* interface IOfflineFilesSetting */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesSetting;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("15F277C6-8600-4941-B47C-FD9F332ADD95")
    IOfflineFilesSetting : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetPreference( 
            /* [out] */ VARIANT *pvarValue,
            /* [in] */ OFFLINEFILES_SETTING_SCOPE scope) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetPreferenceScope( 
            /* [out] */ OFFLINEFILES_SETTING_SCOPE *pScope) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetPreference( 
            /* [in] */ const VARIANT *pvarValue,
            /* [in] */ OFFLINEFILES_SETTING_SCOPE scope) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE DeletePreference( 
            /* [in] */ OFFLINEFILES_SETTING_SCOPE scope) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetPolicy( 
            /* [out] */ VARIANT *pvarValue,
            /* [in] */ OFFLINEFILES_SETTING_SCOPE scope) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetPolicyScope( 
            /* [out] */ OFFLINEFILES_SETTING_SCOPE *pScope) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetValue( 
            /* [out] */ VARIANT *pvarValue,
            /* [out] */ BOOL *pbSetByPolicy) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesSettingVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesSetting * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesSetting * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesSetting * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetPreference )( 
            IOfflineFilesSetting * This,
            /* [out] */ VARIANT *pvarValue,
            /* [in] */ OFFLINEFILES_SETTING_SCOPE scope);
        
        HRESULT ( STDMETHODCALLTYPE *GetPreferenceScope )( 
            IOfflineFilesSetting * This,
            /* [out] */ OFFLINEFILES_SETTING_SCOPE *pScope);
        
        HRESULT ( STDMETHODCALLTYPE *SetPreference )( 
            IOfflineFilesSetting * This,
            /* [in] */ const VARIANT *pvarValue,
            /* [in] */ OFFLINEFILES_SETTING_SCOPE scope);
        
        HRESULT ( STDMETHODCALLTYPE *DeletePreference )( 
            IOfflineFilesSetting * This,
            /* [in] */ OFFLINEFILES_SETTING_SCOPE scope);
        
        HRESULT ( STDMETHODCALLTYPE *GetPolicy )( 
            IOfflineFilesSetting * This,
            /* [out] */ VARIANT *pvarValue,
            /* [in] */ OFFLINEFILES_SETTING_SCOPE scope);
        
        HRESULT ( STDMETHODCALLTYPE *GetPolicyScope )( 
            IOfflineFilesSetting * This,
            /* [out] */ OFFLINEFILES_SETTING_SCOPE *pScope);
        
        HRESULT ( STDMETHODCALLTYPE *GetValue )( 
            IOfflineFilesSetting * This,
            /* [out] */ VARIANT *pvarValue,
            /* [out] */ BOOL *pbSetByPolicy);
        
        END_INTERFACE
    } IOfflineFilesSettingVtbl;

    interface IOfflineFilesSetting
    {
        CONST_VTBL struct IOfflineFilesSettingVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesSetting_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesSetting_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesSetting_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesSetting_GetPreference(This,pvarValue,scope)	\
    (This)->lpVtbl -> GetPreference(This,pvarValue,scope)

#define IOfflineFilesSetting_GetPreferenceScope(This,pScope)	\
    (This)->lpVtbl -> GetPreferenceScope(This,pScope)

#define IOfflineFilesSetting_SetPreference(This,pvarValue,scope)	\
    (This)->lpVtbl -> SetPreference(This,pvarValue,scope)

#define IOfflineFilesSetting_DeletePreference(This,scope)	\
    (This)->lpVtbl -> DeletePreference(This,scope)

#define IOfflineFilesSetting_GetPolicy(This,pvarValue,scope)	\
    (This)->lpVtbl -> GetPolicy(This,pvarValue,scope)

#define IOfflineFilesSetting_GetPolicyScope(This,pScope)	\
    (This)->lpVtbl -> GetPolicyScope(This,pScope)

#define IOfflineFilesSetting_GetValue(This,pvarValue,pbSetByPolicy)	\
    (This)->lpVtbl -> GetValue(This,pvarValue,pbSetByPolicy)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesSetting_GetPreference_Proxy( 
    IOfflineFilesSetting * This,
    /* [out] */ VARIANT *pvarValue,
    /* [in] */ OFFLINEFILES_SETTING_SCOPE scope);


void __RPC_STUB IOfflineFilesSetting_GetPreference_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSetting_GetPreferenceScope_Proxy( 
    IOfflineFilesSetting * This,
    /* [out] */ OFFLINEFILES_SETTING_SCOPE *pScope);


void __RPC_STUB IOfflineFilesSetting_GetPreferenceScope_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSetting_SetPreference_Proxy( 
    IOfflineFilesSetting * This,
    /* [in] */ const VARIANT *pvarValue,
    /* [in] */ OFFLINEFILES_SETTING_SCOPE scope);


void __RPC_STUB IOfflineFilesSetting_SetPreference_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSetting_DeletePreference_Proxy( 
    IOfflineFilesSetting * This,
    /* [in] */ OFFLINEFILES_SETTING_SCOPE scope);


void __RPC_STUB IOfflineFilesSetting_DeletePreference_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSetting_GetPolicy_Proxy( 
    IOfflineFilesSetting * This,
    /* [out] */ VARIANT *pvarValue,
    /* [in] */ OFFLINEFILES_SETTING_SCOPE scope);


void __RPC_STUB IOfflineFilesSetting_GetPolicy_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSetting_GetPolicyScope_Proxy( 
    IOfflineFilesSetting * This,
    /* [out] */ OFFLINEFILES_SETTING_SCOPE *pScope);


void __RPC_STUB IOfflineFilesSetting_GetPolicyScope_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSetting_GetValue_Proxy( 
    IOfflineFilesSetting * This,
    /* [out] */ VARIANT *pvarValue,
    /* [out] */ BOOL *pbSetByPolicy);


void __RPC_STUB IOfflineFilesSetting_GetValue_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesSetting_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0270 */
/* [local] */ 

#define	OFFLINEFILES_SYNC_CONTROL_FLAG_SYNCIN	( 0x1 )

#define	OFFLINEFILES_SYNC_CONTROL_FLAG_SYNCOUT	( 0x2 )

#define	OFFLINEFILES_SYNC_CONTROL_FLAG_PINNEWFILES	( 0x4 )

#define	OFFLINEFILES_SYNC_CONTROL_FLAG_PINLINKTARGETS	( 0x8 )

#define	OFFLINEFILES_SYNC_CONTROL_FLAG_QUERY	( 0x10 )

//


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0270_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0270_v0_0_s_ifspec;

#ifndef __IOfflineFilesSyncControl_INTERFACE_DEFINED__
#define __IOfflineFilesSyncControl_INTERFACE_DEFINED__

/* interface IOfflineFilesSyncControl */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesSyncControl;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3F91289A-10BA-41fd-B85B-11C5F9F62784")
    IOfflineFilesSyncControl : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE QuerySyncAction( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ BOOL *pbSyncIn,
            BOOL *pbSyncOut) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE QueryPinLinkTarget( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ BOOL *pbPinLinkTarget) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE QueryPinNewFiles( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ BOOL *pbPinNewFiles) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesSyncControlVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesSyncControl * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesSyncControl * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesSyncControl * This);
        
        HRESULT ( STDMETHODCALLTYPE *QuerySyncAction )( 
            IOfflineFilesSyncControl * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ BOOL *pbSyncIn,
            BOOL *pbSyncOut);
        
        HRESULT ( STDMETHODCALLTYPE *QueryPinLinkTarget )( 
            IOfflineFilesSyncControl * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ BOOL *pbPinLinkTarget);
        
        HRESULT ( STDMETHODCALLTYPE *QueryPinNewFiles )( 
            IOfflineFilesSyncControl * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ BOOL *pbPinNewFiles);
        
        END_INTERFACE
    } IOfflineFilesSyncControlVtbl;

    interface IOfflineFilesSyncControl
    {
        CONST_VTBL struct IOfflineFilesSyncControlVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesSyncControl_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesSyncControl_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesSyncControl_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesSyncControl_QuerySyncAction(This,pszPath,pbSyncIn,pbSyncOut)	\
    (This)->lpVtbl -> QuerySyncAction(This,pszPath,pbSyncIn,pbSyncOut)

#define IOfflineFilesSyncControl_QueryPinLinkTarget(This,pszPath,pbPinLinkTarget)	\
    (This)->lpVtbl -> QueryPinLinkTarget(This,pszPath,pbPinLinkTarget)

#define IOfflineFilesSyncControl_QueryPinNewFiles(This,pszPath,pbPinNewFiles)	\
    (This)->lpVtbl -> QueryPinNewFiles(This,pszPath,pbPinNewFiles)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesSyncControl_QuerySyncAction_Proxy( 
    IOfflineFilesSyncControl * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [out] */ BOOL *pbSyncIn,
    BOOL *pbSyncOut);


void __RPC_STUB IOfflineFilesSyncControl_QuerySyncAction_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSyncControl_QueryPinLinkTarget_Proxy( 
    IOfflineFilesSyncControl * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [out] */ BOOL *pbPinLinkTarget);


void __RPC_STUB IOfflineFilesSyncControl_QueryPinLinkTarget_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesSyncControl_QueryPinNewFiles_Proxy( 
    IOfflineFilesSyncControl * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [out] */ BOOL *pbPinNewFiles);


void __RPC_STUB IOfflineFilesSyncControl_QueryPinNewFiles_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesSyncControl_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0271 */
/* [local] */ 

//
// IOfflineFilesCache interface
//
// Implemented by Windows.
//
// This interface represents the Offline Files cache to a client of the
// Offline Files service.  Clients create an instance of this object
// in their process.  The object then connects with the running 
// Offline Files service to perform the necessary operations on the
// Offline Files cache.
//
// Method: Synchronize
//
// Call to synchronize one or more files or directories in the cache.
//
// Method: Delete
//
// Call to delete one or more files or directories from the cache.
//
// Method: Pin
//
// Call to pin one or more files or directories to the cache.
//
// Method: Unpin
//
// Call to unpin one or more pinned files or directories in the cache.
//
// Method: GetEncryptionStatus
//
// Call to obtain the current status of cache encryption.  The four states are:
//
//  1. Fully unencrypted.
//  2. Partially unencrypted.
//  3. Partially encrypted.
//  4. Fully encrypted.
//
// Method: Encrypt
//
// Call to encrypt or unencrypt the entire Offline Files cache.
//
// Method: EnumShareRoots
//
// Call to create an enumerator of share root directories in the cache.
// Beginning with each root directory, a client can enumerate the entire 
// cache using IOfflineFile and IEnumOfflineFiles.
// Requires administrative privileges.
//
// Method: FindFile
//
// Call to locate a particular file or directory in the cache and create an
// IOfflineFile instance representing that file or directory.
//
// Method: GetLocation
//
// Call to obtain the current location of the Offline Files cache in
// the local file system.  The string returned is a fully-qualified directory
// path.
//
// Method: MoveCache
//
// Call to move the cache to a new directory or new volume on the local
// computer.  Requires administrative privileges.
//
// Method: GetSettingObject
//
// Call to obtain an IOfflineFilesSetting instance representing one
// Offline Files setting.
//
// Method: QueryStatistics
//
// Call to obtain a set of statistics about part or all of the Offline Files
// cache.


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0271_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0271_v0_0_s_ifspec;

#ifndef __IOfflineFilesCache_INTERFACE_DEFINED__
#define __IOfflineFilesCache_INTERFACE_DEFINED__

/* interface IOfflineFilesCache */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOfflineFilesCache;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B463502C-9A67-420c-9031-BAE5E3B65867")
    IOfflineFilesCache : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE RegisterEventConnection( 
            /* [in] */ IUnknown *punkSink,
            /* [size_is][unique][in] */ OFFLINEFILES_EVENT_FILTER *prgEventFilters,
            /* [in] */ int cEventFilters,
            /* [string][unique][in] */ LPCWSTR pszPathFilter,
            /* [in] */ OFFLINEFILES_PATHFILTER_MATCH PathFilterMatch,
            /* [out] */ DWORD *pdwCookie) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE UnregisterEventConnection( 
            /* [in] */ DWORD dwCookie) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Synchronize( 
            /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
            /* [in] */ int cPaths,
            /* [in] */ BOOL bAsync,
            /* [in] */ DWORD dwSyncControl,
            /* [unique][in] */ IOfflineFilesSyncControl *pISyncControl,
            /* [unique][in] */ IOfflineFilesSyncConflictHandler *pISyncConflictHandler,
            /* [unique][in] */ IOfflineFilesSyncProgress *pIProgress,
            /* [unique][in] */ GUID *pSyncId) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Delete( 
            /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
            /* [in] */ int cPaths,
            /* [in] */ BOOL bAsync,
            /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Pin( 
            /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
            /* [in] */ int cPaths,
            /* [in] */ BOOL bDeep,
            /* [in] */ BOOL bAsync,
            /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Unpin( 
            /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
            /* [in] */ int cPaths,
            /* [in] */ BOOL bDeep,
            /* [in] */ BOOL bAsync,
            /* [in] */ BOOL bDelete,
            /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetEncryptionStatus( 
            /* [out] */ BOOL *pbEncrypted,
            /* [out] */ BOOL *pbPartial) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Encrypt( 
            /* [in] */ BOOL bEncrypt,
            /* [in] */ BOOL bAsync,
            /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE EnumShareRoots( 
            /* [out] */ IEnumOfflineFiles **ppenum) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FindFile( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ IOfflineFile **ppFile) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE RenameDirectory( 
            /* [string][in] */ LPCWSTR pszPathOriginal,
            /* [string][in] */ LPCWSTR pszPathNew,
            /* [in] */ BOOL bReplaceIfExists) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetLocation( 
            /* [string][out] */ LPWSTR *ppszPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE MoveCache( 
            /* [string][in] */ LPCWSTR pszNewLocation,
            BOOL bAsync,
            /* [unique][in] */ IOfflineFilesCacheMoveProgress *pIProgress) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetSettingObject( 
            /* [string][in] */ LPCWSTR pszSettingName,
            /* [out] */ IOfflineFilesSetting **ppSetting) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetCachingMode( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ OFFLINEFILES_CACHING_MODE *pMode) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetDirectoryCachingMode( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ OFFLINEFILES_CACHING_MODE mode) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetDirectoryCachingMode( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ OFFLINEFILES_CACHING_MODE *pMode) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetDirectorySyncMode( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ OFFLINEFILES_SYNC_MODE mode) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetDirectorySyncMode( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ OFFLINEFILES_SYNC_MODE *pMode) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE QueryStatistics( 
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bAllUsers,
            /* [string][size_is][unique][in] */ LPCWSTR *rgpszPathsExcluded,
            /* [in] */ int cPathsExcluded,
            /* [in] */ DWORD dwExcludeFlags,
            /* [in] */ DWORD dwUnityFlags,
            /* [out] */ OFFLINEFILES_STATISTICS *pStatistics) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOfflineFilesCacheVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOfflineFilesCache * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOfflineFilesCache * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOfflineFilesCache * This);
        
        HRESULT ( STDMETHODCALLTYPE *RegisterEventConnection )( 
            IOfflineFilesCache * This,
            /* [in] */ IUnknown *punkSink,
            /* [size_is][unique][in] */ OFFLINEFILES_EVENT_FILTER *prgEventFilters,
            /* [in] */ int cEventFilters,
            /* [string][unique][in] */ LPCWSTR pszPathFilter,
            /* [in] */ OFFLINEFILES_PATHFILTER_MATCH PathFilterMatch,
            /* [out] */ DWORD *pdwCookie);
        
        HRESULT ( STDMETHODCALLTYPE *UnregisterEventConnection )( 
            IOfflineFilesCache * This,
            /* [in] */ DWORD dwCookie);
        
        HRESULT ( STDMETHODCALLTYPE *Synchronize )( 
            IOfflineFilesCache * This,
            /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
            /* [in] */ int cPaths,
            /* [in] */ BOOL bAsync,
            /* [in] */ DWORD dwSyncControl,
            /* [unique][in] */ IOfflineFilesSyncControl *pISyncControl,
            /* [unique][in] */ IOfflineFilesSyncConflictHandler *pISyncConflictHandler,
            /* [unique][in] */ IOfflineFilesSyncProgress *pIProgress,
            /* [unique][in] */ GUID *pSyncId);
        
        HRESULT ( STDMETHODCALLTYPE *Delete )( 
            IOfflineFilesCache * This,
            /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
            /* [in] */ int cPaths,
            /* [in] */ BOOL bAsync,
            /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress);
        
        HRESULT ( STDMETHODCALLTYPE *Pin )( 
            IOfflineFilesCache * This,
            /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
            /* [in] */ int cPaths,
            /* [in] */ BOOL bDeep,
            /* [in] */ BOOL bAsync,
            /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress);
        
        HRESULT ( STDMETHODCALLTYPE *Unpin )( 
            IOfflineFilesCache * This,
            /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
            /* [in] */ int cPaths,
            /* [in] */ BOOL bDeep,
            /* [in] */ BOOL bAsync,
            /* [in] */ BOOL bDelete,
            /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress);
        
        HRESULT ( STDMETHODCALLTYPE *GetEncryptionStatus )( 
            IOfflineFilesCache * This,
            /* [out] */ BOOL *pbEncrypted,
            /* [out] */ BOOL *pbPartial);
        
        HRESULT ( STDMETHODCALLTYPE *Encrypt )( 
            IOfflineFilesCache * This,
            /* [in] */ BOOL bEncrypt,
            /* [in] */ BOOL bAsync,
            /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress);
        
        HRESULT ( STDMETHODCALLTYPE *EnumShareRoots )( 
            IOfflineFilesCache * This,
            /* [out] */ IEnumOfflineFiles **ppenum);
        
        HRESULT ( STDMETHODCALLTYPE *FindFile )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ IOfflineFile **ppFile);
        
        HRESULT ( STDMETHODCALLTYPE *RenameDirectory )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszPathOriginal,
            /* [string][in] */ LPCWSTR pszPathNew,
            /* [in] */ BOOL bReplaceIfExists);
        
        HRESULT ( STDMETHODCALLTYPE *GetLocation )( 
            IOfflineFilesCache * This,
            /* [string][out] */ LPWSTR *ppszPath);
        
        HRESULT ( STDMETHODCALLTYPE *MoveCache )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszNewLocation,
            BOOL bAsync,
            /* [unique][in] */ IOfflineFilesCacheMoveProgress *pIProgress);
        
        HRESULT ( STDMETHODCALLTYPE *GetSettingObject )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszSettingName,
            /* [out] */ IOfflineFilesSetting **ppSetting);
        
        HRESULT ( STDMETHODCALLTYPE *GetCachingMode )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ OFFLINEFILES_CACHING_MODE *pMode);
        
        HRESULT ( STDMETHODCALLTYPE *SetDirectoryCachingMode )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ OFFLINEFILES_CACHING_MODE mode);
        
        HRESULT ( STDMETHODCALLTYPE *GetDirectoryCachingMode )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ OFFLINEFILES_CACHING_MODE *pMode);
        
        HRESULT ( STDMETHODCALLTYPE *SetDirectorySyncMode )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ OFFLINEFILES_SYNC_MODE mode);
        
        HRESULT ( STDMETHODCALLTYPE *GetDirectorySyncMode )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [out] */ OFFLINEFILES_SYNC_MODE *pMode);
        
        HRESULT ( STDMETHODCALLTYPE *QueryStatistics )( 
            IOfflineFilesCache * This,
            /* [string][in] */ LPCWSTR pszPath,
            /* [in] */ BOOL bAllUsers,
            /* [string][size_is][unique][in] */ LPCWSTR *rgpszPathsExcluded,
            /* [in] */ int cPathsExcluded,
            /* [in] */ DWORD dwExcludeFlags,
            /* [in] */ DWORD dwUnityFlags,
            /* [out] */ OFFLINEFILES_STATISTICS *pStatistics);
        
        END_INTERFACE
    } IOfflineFilesCacheVtbl;

    interface IOfflineFilesCache
    {
        CONST_VTBL struct IOfflineFilesCacheVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOfflineFilesCache_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOfflineFilesCache_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOfflineFilesCache_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOfflineFilesCache_RegisterEventConnection(This,punkSink,prgEventFilters,cEventFilters,pszPathFilter,PathFilterMatch,pdwCookie)	\
    (This)->lpVtbl -> RegisterEventConnection(This,punkSink,prgEventFilters,cEventFilters,pszPathFilter,PathFilterMatch,pdwCookie)

#define IOfflineFilesCache_UnregisterEventConnection(This,dwCookie)	\
    (This)->lpVtbl -> UnregisterEventConnection(This,dwCookie)

#define IOfflineFilesCache_Synchronize(This,rgpszPaths,cPaths,bAsync,dwSyncControl,pISyncControl,pISyncConflictHandler,pIProgress,pSyncId)	\
    (This)->lpVtbl -> Synchronize(This,rgpszPaths,cPaths,bAsync,dwSyncControl,pISyncControl,pISyncConflictHandler,pIProgress,pSyncId)

#define IOfflineFilesCache_Delete(This,rgpszPaths,cPaths,bAsync,pIProgress)	\
    (This)->lpVtbl -> Delete(This,rgpszPaths,cPaths,bAsync,pIProgress)

#define IOfflineFilesCache_Pin(This,rgpszPaths,cPaths,bDeep,bAsync,pIProgress)	\
    (This)->lpVtbl -> Pin(This,rgpszPaths,cPaths,bDeep,bAsync,pIProgress)

#define IOfflineFilesCache_Unpin(This,rgpszPaths,cPaths,bDeep,bAsync,bDelete,pIProgress)	\
    (This)->lpVtbl -> Unpin(This,rgpszPaths,cPaths,bDeep,bAsync,bDelete,pIProgress)

#define IOfflineFilesCache_GetEncryptionStatus(This,pbEncrypted,pbPartial)	\
    (This)->lpVtbl -> GetEncryptionStatus(This,pbEncrypted,pbPartial)

#define IOfflineFilesCache_Encrypt(This,bEncrypt,bAsync,pIProgress)	\
    (This)->lpVtbl -> Encrypt(This,bEncrypt,bAsync,pIProgress)

#define IOfflineFilesCache_EnumShareRoots(This,ppenum)	\
    (This)->lpVtbl -> EnumShareRoots(This,ppenum)

#define IOfflineFilesCache_FindFile(This,pszPath,ppFile)	\
    (This)->lpVtbl -> FindFile(This,pszPath,ppFile)

#define IOfflineFilesCache_RenameDirectory(This,pszPathOriginal,pszPathNew,bReplaceIfExists)	\
    (This)->lpVtbl -> RenameDirectory(This,pszPathOriginal,pszPathNew,bReplaceIfExists)

#define IOfflineFilesCache_GetLocation(This,ppszPath)	\
    (This)->lpVtbl -> GetLocation(This,ppszPath)

#define IOfflineFilesCache_MoveCache(This,pszNewLocation,bAsync,pIProgress)	\
    (This)->lpVtbl -> MoveCache(This,pszNewLocation,bAsync,pIProgress)

#define IOfflineFilesCache_GetSettingObject(This,pszSettingName,ppSetting)	\
    (This)->lpVtbl -> GetSettingObject(This,pszSettingName,ppSetting)

#define IOfflineFilesCache_GetCachingMode(This,pszPath,pMode)	\
    (This)->lpVtbl -> GetCachingMode(This,pszPath,pMode)

#define IOfflineFilesCache_SetDirectoryCachingMode(This,pszPath,mode)	\
    (This)->lpVtbl -> SetDirectoryCachingMode(This,pszPath,mode)

#define IOfflineFilesCache_GetDirectoryCachingMode(This,pszPath,pMode)	\
    (This)->lpVtbl -> GetDirectoryCachingMode(This,pszPath,pMode)

#define IOfflineFilesCache_SetDirectorySyncMode(This,pszPath,mode)	\
    (This)->lpVtbl -> SetDirectorySyncMode(This,pszPath,mode)

#define IOfflineFilesCache_GetDirectorySyncMode(This,pszPath,pMode)	\
    (This)->lpVtbl -> GetDirectorySyncMode(This,pszPath,pMode)

#define IOfflineFilesCache_QueryStatistics(This,pszPath,bAllUsers,rgpszPathsExcluded,cPathsExcluded,dwExcludeFlags,dwUnityFlags,pStatistics)	\
    (This)->lpVtbl -> QueryStatistics(This,pszPath,bAllUsers,rgpszPathsExcluded,cPathsExcluded,dwExcludeFlags,dwUnityFlags,pStatistics)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IOfflineFilesCache_RegisterEventConnection_Proxy( 
    IOfflineFilesCache * This,
    /* [in] */ IUnknown *punkSink,
    /* [size_is][unique][in] */ OFFLINEFILES_EVENT_FILTER *prgEventFilters,
    /* [in] */ int cEventFilters,
    /* [string][unique][in] */ LPCWSTR pszPathFilter,
    /* [in] */ OFFLINEFILES_PATHFILTER_MATCH PathFilterMatch,
    /* [out] */ DWORD *pdwCookie);


void __RPC_STUB IOfflineFilesCache_RegisterEventConnection_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_UnregisterEventConnection_Proxy( 
    IOfflineFilesCache * This,
    /* [in] */ DWORD dwCookie);


void __RPC_STUB IOfflineFilesCache_UnregisterEventConnection_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_Synchronize_Proxy( 
    IOfflineFilesCache * This,
    /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
    /* [in] */ int cPaths,
    /* [in] */ BOOL bAsync,
    /* [in] */ DWORD dwSyncControl,
    /* [unique][in] */ IOfflineFilesSyncControl *pISyncControl,
    /* [unique][in] */ IOfflineFilesSyncConflictHandler *pISyncConflictHandler,
    /* [unique][in] */ IOfflineFilesSyncProgress *pIProgress,
    /* [unique][in] */ GUID *pSyncId);


void __RPC_STUB IOfflineFilesCache_Synchronize_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_Delete_Proxy( 
    IOfflineFilesCache * This,
    /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
    /* [in] */ int cPaths,
    /* [in] */ BOOL bAsync,
    /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress);


void __RPC_STUB IOfflineFilesCache_Delete_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_Pin_Proxy( 
    IOfflineFilesCache * This,
    /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
    /* [in] */ int cPaths,
    /* [in] */ BOOL bDeep,
    /* [in] */ BOOL bAsync,
    /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress);


void __RPC_STUB IOfflineFilesCache_Pin_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_Unpin_Proxy( 
    IOfflineFilesCache * This,
    /* [string][size_is][in] */ LPCWSTR *rgpszPaths,
    /* [in] */ int cPaths,
    /* [in] */ BOOL bDeep,
    /* [in] */ BOOL bAsync,
    /* [in] */ BOOL bDelete,
    /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress);


void __RPC_STUB IOfflineFilesCache_Unpin_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_GetEncryptionStatus_Proxy( 
    IOfflineFilesCache * This,
    /* [out] */ BOOL *pbEncrypted,
    /* [out] */ BOOL *pbPartial);


void __RPC_STUB IOfflineFilesCache_GetEncryptionStatus_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_Encrypt_Proxy( 
    IOfflineFilesCache * This,
    /* [in] */ BOOL bEncrypt,
    /* [in] */ BOOL bAsync,
    /* [unique][in] */ IOfflineFilesSimpleProgress *pIProgress);


void __RPC_STUB IOfflineFilesCache_Encrypt_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_EnumShareRoots_Proxy( 
    IOfflineFilesCache * This,
    /* [out] */ IEnumOfflineFiles **ppenum);


void __RPC_STUB IOfflineFilesCache_EnumShareRoots_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_FindFile_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [out] */ IOfflineFile **ppFile);


void __RPC_STUB IOfflineFilesCache_FindFile_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_RenameDirectory_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszPathOriginal,
    /* [string][in] */ LPCWSTR pszPathNew,
    /* [in] */ BOOL bReplaceIfExists);


void __RPC_STUB IOfflineFilesCache_RenameDirectory_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_GetLocation_Proxy( 
    IOfflineFilesCache * This,
    /* [string][out] */ LPWSTR *ppszPath);


void __RPC_STUB IOfflineFilesCache_GetLocation_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_MoveCache_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszNewLocation,
    BOOL bAsync,
    /* [unique][in] */ IOfflineFilesCacheMoveProgress *pIProgress);


void __RPC_STUB IOfflineFilesCache_MoveCache_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_GetSettingObject_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszSettingName,
    /* [out] */ IOfflineFilesSetting **ppSetting);


void __RPC_STUB IOfflineFilesCache_GetSettingObject_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_GetCachingMode_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [out] */ OFFLINEFILES_CACHING_MODE *pMode);


void __RPC_STUB IOfflineFilesCache_GetCachingMode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_SetDirectoryCachingMode_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [in] */ OFFLINEFILES_CACHING_MODE mode);


void __RPC_STUB IOfflineFilesCache_SetDirectoryCachingMode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_GetDirectoryCachingMode_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [out] */ OFFLINEFILES_CACHING_MODE *pMode);


void __RPC_STUB IOfflineFilesCache_GetDirectoryCachingMode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_SetDirectorySyncMode_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [in] */ OFFLINEFILES_SYNC_MODE mode);


void __RPC_STUB IOfflineFilesCache_SetDirectorySyncMode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_GetDirectorySyncMode_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [out] */ OFFLINEFILES_SYNC_MODE *pMode);


void __RPC_STUB IOfflineFilesCache_GetDirectorySyncMode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IOfflineFilesCache_QueryStatistics_Proxy( 
    IOfflineFilesCache * This,
    /* [string][in] */ LPCWSTR pszPath,
    /* [in] */ BOOL bAllUsers,
    /* [string][size_is][unique][in] */ LPCWSTR *rgpszPathsExcluded,
    /* [in] */ int cPathsExcluded,
    /* [in] */ DWORD dwExcludeFlags,
    /* [in] */ DWORD dwUnityFlags,
    /* [out] */ OFFLINEFILES_STATISTICS *pStatistics);


void __RPC_STUB IOfflineFilesCache_QueryStatistics_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOfflineFilesCache_INTERFACE_DEFINED__ */


/* interface __MIDL_itf_cscproxy_0272 */
/* [local] */ 


extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0274_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_cscproxy_0274_v0_0_s_ifspec;

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  VARIANT_UserSize(     unsigned long *, unsigned long            , VARIANT * ); 
unsigned char * __RPC_USER  VARIANT_UserMarshal(  unsigned long *, unsigned char *, VARIANT * ); 
unsigned char * __RPC_USER  VARIANT_UserUnmarshal(unsigned long *, unsigned char *, VARIANT * ); 
void                      __RPC_USER  VARIANT_UserFree(     unsigned long *, VARIANT * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif



