#include "stdafx.h"

#include "MainWindow.xaml.h"

void animateproperty_mcpp::MainWindow::WindowLoaded(System::Object __gc *sender, System::EventArgs __gc *e){

	

	// Create and set the button.
	System::Windows::Controls::Button *aButton = 
		new System::Windows::Controls::Button();
	System::Windows::Controls::Canvas::SetLeft(aButton, System::Windows::Length(20));
	System::Windows::Controls::Canvas::SetTop(aButton, System::Windows::Length(20));
	aButton->set_Width(System::Windows::Length(200));
	aButton->set_Height(System::Windows::Length(30));
	aButton->Content = new System::String("A Button");


	// Animate the button's width.
	System::Windows::Media::Animation::LengthAnimation * myLengthAnimation =
		new System::Windows::Media::Animation::LengthAnimation();
	myLengthAnimation->set_To(System::Windows::Length(50));
	myLengthAnimation->set_Duration(System::Windows::Media::Animation::Time(5000));
	myLengthAnimation->set_AutoReverse(true);
	myLengthAnimation->set_RepeatDuration(System::Windows::Media::Animation::Time::Indefinite);

	// Add the animation to the property.
	aButton->AddAnimation(System::Windows::Controls::Button::WidthProperty, myLengthAnimation);

	// Add the button to the canvas.
	myCanvas->get_Children()->Add(aButton);

	// Create and set the second button.
	System::Windows::Controls::Button *anotherButton = 
		new System::Windows::Controls::Button();
	System::Windows::Controls::Canvas::SetLeft(anotherButton, System::Windows::Length(20));
	System::Windows::Controls::Canvas::SetTop(anotherButton, System::Windows::Length(70));
	anotherButton->set_Width(System::Windows::Length(200));
	anotherButton->set_Height(System::Windows::Length(30));
	anotherButton->Content = new System::String("Another Button");

	// Create and animate a Brush to set the Button's fill.
	System::Windows::Media::SolidColorBrush * myBrush = 
		new System::Windows::Media::SolidColorBrush();
	myBrush->Color = System::Windows::Media::Colors::Blue;
	
	System::Windows::Media::Animation::ColorAnimation * myColorAnimation =
		new System::Windows::Media::Animation::ColorAnimation();
	myColorAnimation->set_From(System::Windows::Media::Colors::Blue);
	myColorAnimation->set_To(System::Windows::Media::Colors::Red);
	myColorAnimation->set_Duration(System::Windows::Media::Animation::Time(7000));
	myColorAnimation->set_AutoReverse(true);
	myColorAnimation->set_RepeatDuration(System::Windows::Media::Animation::Time::Indefinite);

	myBrush->get_ColorAnimations()->Add(myColorAnimation);

	anotherButton->set_Background(myBrush);

	myCanvas->get_Children()->Add(anotherButton);


}
