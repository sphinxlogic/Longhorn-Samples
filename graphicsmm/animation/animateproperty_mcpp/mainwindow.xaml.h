#pragma once

//This is a list of commonly used namespaces for a window.
using namespace System;
using namespace System::Windows;
using namespace System::Windows::Controls;
using namespace System::Windows::Documents;
using namespace System::Windows::Navigation;
using namespace System::Windows::Shapes;
using namespace System::Windows::Data;

namespace animateproperty_mcpp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public __gc class MainWindow : public MainWindowBase
    {
        // To use Loaded event put Loaded="WindowLoaded" attribute in root element of MainWindow.xaml
        protected: void WindowLoaded(Object* sender, EventArgs* e);
        // Sample event handler:  
        // protected: void ButtonClick(Object* sender, ClickEventArgs* e) {}
    };
}