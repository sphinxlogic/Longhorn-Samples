Namespace WCPSample 
	Public Class SimpleBinding 
	Private _simpleProperty As String = "This is a string from the data source"

	Public ReadOnly Property SimpleProperty() As String
            Get
            Return _simpleProperty
            End Get
        End Property
	
	Public SimpleBinding()	
    
        End Class

End Namespace