#pragma once

//This is a list of commonly used namespaces for an application class.
using namespace System;
using namespace System::Windows;
using namespace System::Data;
using namespace System::Xml;
using namespace System::Configuration;

namespace animateproperty_mcpp
{
    /// <summary>
    /// Interaction logic for MyApp.xaml
    /// </summary>
    public __gc class MyApp : public MyAppBase
    {
		protected: void AppStartingUp(Object* sender, StartingUpCancelEventArgs* e);
    };
}