#include "stdafx.h"

#include "MyApp.xaml.h"
#include "MainWindow.xaml.h"

using namespace animateproperty_mcpp;

void MyApp::AppStartingUp(Object* sender, StartingUpCancelEventArgs* e)
{
	animateproperty_mcpp::MainWindow* mainWindow = new animateproperty_mcpp::MainWindow();
	mainWindow->Show();
}