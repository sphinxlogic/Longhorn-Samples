//This is a list of commonly used namespaces for a window.
using System;
using System.Collections;                       // IList
using System.Diagnostics;
using System.Xml;                               // XmlQualifiedName
using System.Windows;
using System.Windows.Controls;
using System.Windows.Design;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Media;                     // ImageData
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;
using System.Windows.Annotations.Component;
using System.Windows.Annotations.Anchoring;

namespace HelloAnnotations
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

            // register the TextSelectionProcessor
            service.LocatorManager.RegisterSelectionProcessor(
                new TextSelectionProcessor(service.LocatorManager),
                typeof(TextRange),
                new XmlQualifiedName[]
                    { new XmlQualifiedName("CharacterRange",
                      AnnotationStore.BaseSchemaNamespaceUri) } );

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

        } // Init()


        /// CreateAnnotation
        /// <summary>
        ///   Creates an annotation on a specified textBox selection.
        /// </summary>
        private void CreateAnnotation(
            object sender, System.Windows.Controls.ClickEventArgs args)
        {
            if (textBox.Selection.IsEmpty)
            {
                MessageBox.Show("Please select text to annotate.");
                return;  // nothing selected, nothing to annotate
            }

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

            // generate a Locator list for the text selection
            object currentSelection = new TextRange(textBox.Selection.Start,
                                                    textBox.Selection.End);
            IList locators =
                service.LocatorManager.GenerateLocators(currentSelection);

            // create an anchor and add the selection locators to it
            Resource anchor = store.CreateResource();
            foreach (object locator in locators)
                anchor.Locators.Add(locator);

            // add the anchor to the annotation
            annotation.Anchors.Add(anchor);

            // add the annotation to the AnnotationStore
            store.AddAnnotation(annotation);

            // Done - at this point all further operations are
            // handled by the Microsoft Annotations Framework.

        } // CreateAnnotation()


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
                "//" + AnnotationStore.DefaultSchemaPrefix + ":Annotation");

            // delete each annotation in the list
            foreach (Annotation anno in annotations)
                store.DeleteAnnotation(anno.Id);
        }


        /// MyAnnotationPanel
        /// <summary>
        ///   The AnnotationPanel is a transparent layer over the application
        ///   window.  The AnnotationPanel manages a list of Annotation-
        ///   Components that display annotations floating over the application
        ///   window.
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

        } // end:class MyAnnotationPanel

    } // end:class Window1

} // end:namespace HelloAnnotations
