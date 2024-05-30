//This is a list of commonly used namespaces for a window.
using System;
using System.Collections;                       // IList
using System.Diagnostics;
using System.Xml;                               // XmlQualifiedName
using System.Windows;
using System.Windows.Controls;
using System.Windows.Design;
using System.Windows.Documents;
using System.Windows.Data;
using System.Windows.Input;                     // MouseButtonEventArgs
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;
using System.Windows.Annotations.Component;
using System.Windows.Annotations.Anchoring;

namespace FormAnnotations
{
    /// <summary>
    ///   Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        // Initialize and configure the Microsoft Annotations Framework
        private void Init(object sender, EventArgs args)
        {
            // create an annotation panel that overlays the topCanvas
            MyAnnotationPanel panel = new MyAnnotationPanel();
            topCanvas.Children.Add(panel);

            // create the annotation service
            AnnotationService service = new AnnotationService();

            // register the TreeNodeSelectionProcessor
            service.LocatorManager.RegisterSelectionProcessor(
                new TreeNodeSelectionProcessor(service.LocatorManager),
                typeof(TextBox),
                new XmlQualifiedName[0]);

            // set the annotation service with the ServiceManager
            ServiceManager.SetService(typeof(AnnotationService),
                                        topLevel, service);

            // set the annotation panel scope to the topLevel node
            // (setting panel.scope must be performed between
            //  ServiceManager.Service and IScopedService.Attach)
            panel.Scope = topLevel;

            // attach the annotation service to the topLevel
            ((IScopedService)service).Attach(topLevel);

            // output the annotation store on every update
            AnnotationService.GetStore(topLevel).AutoFlush = true;

        } // end:Init()


        /// MyAnnotationPanel
        /// <summary>
        ///   The AnnotationPanel is a transparent layer over the application
        ///   window.  The AnnotationPanel manages a list of Annotation-
        ///   Components that display annotations floating on top of the
        ///   application window.
        /// </summary>
        private class MyAnnotationPanel : AnnotationPanel
        {
            /// ChooseAnnotationComponent
            /// <summary>
            ///   Chooses an appropriate AnnotationComponent for displaying
            ///   a user annotation.</summary>
            /// <remarks>
            ///   In this example there is only one AnnotationComponent
            ///   type, MiniNoteAnnotation.</remarks>
            public override IList ChooseAnnotationComponent(
                                      IAttachedAnnotation attachedAnnotation)
            {
                Debug.Assert(attachedAnnotation != null);

                // create a generic component that can be of any type
                IAttachedAnnotationComponent component = null;

                // Based on the Annotation.TypeName specified in the
                // application's CreateAnnotation method, create an
                // AnnotationComponent to display and manage this
                // annotation on the AnnotationPanel.
                if (attachedAnnotation.Annotation.TypeName == "MiniNote")
                {
                    // create a MiniNote annotation component
                    component = new MiniNoteAnnotation();

                    // add the new attachedAnnotation to the component
                    component.AddAttachedAnnotation(attachedAnnotation);

                    // add the component as a child of the AnnotationPanel
                    this.Children.Add((MiniNoteAnnotation)component);
                }
                else
                {   // Normally code would go here to handle the
                    // case of an undefined Annotation.TypeName
                }   // (in this example there's only MiniNote).

                return new IAttachedAnnotationComponent[] { component };
            }
        }


        /// OnMouseRightButton
        /// <summary>
        ///   Handles MouseRightButtonDown events for creating
        ///   annotations on TextBox selections.</summary>
        private void OnMouseRightButton(object sender, MouseButtonEventArgs e)
        {
            // create an annotation on the clicked element
            CreateAnnotation((FrameworkElement)sender);
        }


        /// CreateAnnotation
        /// <summary>
        ///   Creates an annotation on a specified FrameworkElement.
        /// </summary>
        private void CreateAnnotation(FrameworkElement annotationElement)
        {
            Debug.Assert(annotationElement != null);

            // get a reference to the AnnotationService
            AnnotationService service =
                (AnnotationService)ServiceManager.GetService(
                    typeof(AnnotationService), topLevel);
            Debug.Assert(service != null);

            // get a reference to the AnnotationStore
            AnnotationStore store = AnnotationService.GetStore(topLevel);
            Debug.Assert(store != null);

            // create a new annotation
            Annotation annotation = store.CreateAnnotation();

            // specify the type of annotation we want
            annotation.TypeName = "MiniNote";

            // generate a Locator list for the selected element
            IList locators =
                service.LocatorManager.GenerateLocators(annotationElement);

            // create an anchor resource containing the selection locators
            Resource anchor = store.CreateResource();
            foreach (object locator in locators)
                anchor.Locators.Add(locator);

            // add the anchor to the annotation
            annotation.Anchors.Add(anchor);

            // add the annotation to the AnnotationStore
            store.AddAnnotation(annotation);

            // Done - at this point all further operations are
            // handled by the Microsoft Annotations Framework.

        } // end:CreateAnnotation()


        /// OnClickClearAllAnnotations
        /// <summary>
        ///   Removes all annotations currently in the annotation store.
        /// </summary>
        private void OnClickClearAllAnnotations(object sender,
            System.Windows.Controls.ClickEventArgs args)
        {
            // get a reference to the annoation store
            AnnotationStore store = AnnotationService.GetStore(topLevel);
            Debug.Assert(store != null);

            // get the current list of annotations
            IList annotations = store.FindAnnotations(
                "//" + AnnotationStore.DefaultSchemaPrefix + ":Annotation" );

            // delete each annotation in the list
            foreach (Annotation anno in annotations)
                store.DeleteAnnotation(anno.Id);
        }


        /// OnClickNext
        /// <summary>Moves to display the next data record.</summary>
        private void OnClickNext(object sender, ClickEventArgs e)
        {
            Binding.GetView(mainGridPanel.DataContext).CurrentItem.MoveNext();
            // if after the last record, wrap back to the first
            if (Binding.GetView(mainGridPanel.DataContext).CurrentItem.IsAfterLast)
                Binding.GetView(mainGridPanel.DataContext).CurrentItem.MoveFirst();
        }

        /// OnClickPrevious
        /// <summary>Moves to display the previous data record.</summary>
        private void OnClickPrevious(object sender, ClickEventArgs e)
        {
            Binding.GetView(mainGridPanel.DataContext).CurrentItem.MovePrevious();
            // if before the first record, wrap to the last
            if (Binding.GetView(mainGridPanel.DataContext).CurrentItem.IsBeforeFirst)
                Binding.GetView(mainGridPanel.DataContext).CurrentItem.MoveLast();
        }

    } // end:class Window1

} // end:namespace FormAnnotations
