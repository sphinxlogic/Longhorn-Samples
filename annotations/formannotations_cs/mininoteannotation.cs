//-----------------------------------------------------------------------------
//
// <copyright file="MiniNoteAnnotation.cs" company="Microsoft">
//   Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//   MiniNoteAnnotation - Annotation component to display a simple
//   user-editable mini text note annotation.
//
// History:
//   23-Feb-2004 a-jackd: Initial implementation
//
//-----------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;                       // XmlDocument
using System.Windows.Design;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Input;             // FocusChangedEventArgs
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Serialization;


namespace System.Windows.Annotations.Component
{
    /// MiniNoteAnnotation
    /// <summary>
    ///   Manages and displays simple user-editable MiniNote annotations.
    /// </summary>
    public class MiniNoteAnnotation : DockPanel, IAttachedAnnotationComponent
    {
        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        #region Constructors

        /// <summary>
        ///   Creates an instance of a TextNoteAnnotation component
        ///   and initializes its layout.
        /// </summary>
        internal MiniNoteAnnotation()
        {
            // instantiate and initialize a TextBox to display the user note
            _textbox        = new TextBox();
            _textbox.Width  = new Length(80);
            _textbox.Height = new Length(40);
            _textbox.Wrap   = true;
            _textbox.Background = new SolidColorBrush(
                                      Color.FromARGB(0xFF, 0xFB, 0xF7, 0xC8));

            // set a default user annotation message
            _textbox.Text = _defaultAnnotationText;

            // set the textbox.LostFocus handler for user update edits
            _textbox.LostFocus += new FocusChangedEventHandler(this.OnLostFocus);

            // set the DocPanel.Dock property to "Fill"
            DockPanel.SetDock(_textbox, Dock.Fill);

            // add the textbox as a child of this annotation component
            this.Children.Add(_textbox);
        }

        #endregion Constructors


        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Public Operators
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Public Events
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------

        #region Internal Methods

        /// <summary>
        ///   Adds an attachedAnnotation to the list of attachedAnnotation
        ///   annotations this component represents.</summary>
        /// <param name="attachedAnnotation">
        ///   The attachedAnnotation to be added to the annotations
        ///   rendered by this component.</param>
        /// <exception cref="ArgumentNullException">
        ///   The exception that is thrown when attachedAnnotation is null.
        /// </exception>
        /// <remarks>
        ///   Within the "else" below when loading existing annotations, the
        ///   annotation Cargo Content that contains the MiniNote text may be
        ///   at any location in lists of Cargos and Cargo.Contents.  The
        ///   first "for" loop searches through the list of Cargos, while the
        ///   second "for" then searches the Contents list within each cargo.
        /// </remarks>
        public void AddAttachedAnnotation(IAttachedAnnotation attachedAnnotation)
        {
            if (attachedAnnotation == null)
                throw new ArgumentNullException("attachedAnnotation");

            // if the annotation is being created (has no cargo yet),
            // create a cargo containing the user text in the textbox.
            if (attachedAnnotation.Annotation.Cargos.Count == 0)
            {
                // create a cargo resource for the user annotation data
                Resource cargo = attachedAnnotation.Store.CreateResource();

                // create an xml element to hold the user annotation note
                XmlDocument xmlDoc = new XmlDocument();
                _textnoteContent = xmlDoc.CreateElement("TextNote", _MiniNoteURI);

                // set the Xml data to the text contained in the textbox
                _textnoteContent.InnerXml = _textbox.Text;

                // add the xml element as the cargo content
                cargo.Contents.Add(_textnoteContent);

                // add the cargo to the annotation
                attachedAnnotation.Annotation.Cargos.Add(cargo);
            }

            // if the annotation is being reloaded (already has a cargo),
            // set the textbox text with the annotation's cargo text
            else  // if (attachedAnnotation.Annotation.Cargos.Count > 0)
            {
                // Search each cargo in the annotation.
                foreach (Resource cargo in attachedAnnotation.Annotation.Cargos)
                {
                    // Search each content within each cargo.
                    foreach (XmlElement content in cargo.Contents)
                    {
                        // Check if the content XmlElement matches
                        // the Name and URI for a MiniNote.
                        if ( (content.Name == "TextNote")
                          && (content.NamespaceURI == _MiniNoteURI) )
                        {
                            // We've found the valid content element.
                            // Set the textbox text with the content.
                            _textbox.Text = content.InnerXml;

                            // Save the reference to the content.
                            _textnoteContent = content;
                        }
                    } // end:foreach (Object content
                } // end:foreach (Resource cargo
            } // end:else // if (attachedAnnotation.Annotation.Cargos.Count > 0)

            // set the attachedAnnotation as this component's _attachedAnnotation
            _attachedAnnotation = attachedAnnotation;

        } // end:AddAttachedAnnotation()


        /// <summary>
        ///   Modifies an attached annotation held by this component.
        /// </summary>
        /// <param name="originalAttachedAnnotation">
        ///   The original attached annotation this component holds.</param>
        /// <param name="attachedAnnotation">
        ///   The new attached annotation this component is to hold.</param>
        /// <exception cref="ArgumentNullException">
        ///   The exception that is thrown then attachedAnnotation or
        ///   originalAttachedAnnotation is null.</exception>
        public void ModifyAttachedAnnotation(
            IAttachedAnnotation oldAttachedAnnotation,
            IAttachedAnnotation newAttachedAnnotation)
        {
            if (oldAttachedAnnotation == null)
                throw new ArgumentNullException("oldAttachedAnnotation");
            if (newAttachedAnnotation == null)
                throw new ArgumentNullException("newAttachedAnnotation");
            Debug.Assert(_attachedAnnotation != null);

            // set the newAttachedAnnotation as component's _attachedAnnotation
            _attachedAnnotation = newAttachedAnnotation;
        }


        /// <summary>
        ///   Removes the IAttachedAnnotation held by this component.
        /// </summary>
        /// <param name="attachedAnnotation">
        ///   The attached annotation to be removed.</param>
        /// <exception cref="ArgumentNullException">
        ///   The exception that is thrown when attachedAnnotation is null.
        /// </exception>
        public void RemoveAttachedAnnotation(IAttachedAnnotation attachedAnnotation)
        {
            if (attachedAnnotation == null)
                throw new ArgumentNullException("attachedAnnotation");
            Debug.Assert(_attachedAnnotation == attachedAnnotation);

            if (_attachedAnnotation != null)
                _attachedAnnotation = null;

            _textbox.Text = _defaultAnnotationText;
            _textnoteContent = null;
        }
        #endregion Internal Methods


        //------------------------------------------------------
        //
        //  Internal Properties
        //
        //------------------------------------------------------

        #region Internal Properties

        /// <summary>
        ///   Gets a copy of the list of attached annotations
        ///   this component is representing.
        /// </summary>
        /// <returns>
        ///   Returns a list of the IAttachedAnnotation
        ///   instances this component is representing.
        /// </returns>
        public IList AttachedAnnotations
        {
            get
            {
                if (_attachedAnnotation != null)
                {
                    ArrayList attachedAnnotations = new ArrayList(1);
                    attachedAnnotations.Add(_attachedAnnotation);
                    return (IList) attachedAnnotations.Clone();
                }
                else
                    return new ArrayList(0);
            }
        }

        #endregion Internal Properties


        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------

        #region Private Methods
        /// OnLostFocus
        /// <summary>
        ///   Updates the annotation cargo when the user switches
        ///   focus after modifying the text in the annotation textbox.
        /// </summary>
        /// <remarks>
        ///   The Annotation Cargo Content that contains the MiniNote text
        ///   may be at any location in lists of Cargos and Cargo.Contents.
        ///   The first "for" loop searches through the list of Cargos, while
        ///   the second "for" then searches the Contents list within each
        ///   cargo.
        /// </remarks>
        private void OnLostFocus(object sender, FocusChangedEventArgs e)
        {
            Debug.Assert(sender == _textbox);  // sender should be our textbox
            Debug.Assert(_textnoteContent != null);  // content should be set

            // Store the updated user text in the annotation content.
            _textnoteContent.InnerXml = ((TextBox)sender).Text;

            // Save the updated annotation in the store.
            _attachedAnnotation.Store.ModifyAnnotation(
                                               _attachedAnnotation.Annotation);
        } // end:OnLostFocus()
        #endregion Private Methods


        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------

        #region Private Fields

        // the attached annotation rendered by this component
        private IAttachedAnnotation _attachedAnnotation = null;

        // AttachedAnnotation's cargo content for this component
        private XmlElement _textnoteContent = null;

        // a TextBox to hold and display the annotation
        private TextBox _textbox = null;

        // sample namespaceURI (mininote is not an offical Microsoft schema)
        private const String _MiniNoteURI =
            "http://schemas.microsoft.com/windows/annotations/2004/03/mininote";

        // default message text for a new annotation
        private const String _defaultAnnotationText = "Here's an annotation.";

        #endregion Private Fields


    } // end:public class MiniNoteAnnotation

} // end:namespace System.Windows.Annotations.Component
