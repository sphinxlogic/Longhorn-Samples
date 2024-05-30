//------------------------------------------------------------------------------
// XAML code behind file
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Data;
using System.Windows.Media;

namespace SDKSample
{
  /// <summary>
  /// NumberListItem. This is the class that encapsulates each item
  /// in the numberlist.  The NLValue property is the property
  /// that is bound to by UI elements in the XAML markup.
  /// </summary>
  public class NumberListItem : IPropertyChange
  {
    private int _NLValue;

    public NumberListItem()
    {
    }

    public int NLValue
    {
      get
      {
        return _NLValue;
      }

      set
      {
        if (_NLValue != value)
        {
          _NLValue = value;
          NotifyPropertyChanged("NLValue");
        }
      }
    }

    // The following variable and method provide the support for
    // handling property changed events.
    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(String info)
    {
    	if (PropertyChanged != null)
    		PropertyChanged(this, new PropertyChangedEventArgs(info));
    }
  }

  /// <summary>
  /// NumberList. In addition to serving as the datasource for the
  /// binding in this program, this class also exposes the methods
  /// through which a change to the list content can be initiated.
  /// </summary>
  public class NumberList : ArrayListDataCollection
  {
    public NumberList() : base(3)
    {
      Add(new NumberListItem());
      Add(new NumberListItem());
      Add(new NumberListItem());
    }

    public void SetOdd()
    {
      // Each of these NLValue assignments causes a NotifyPropertyChanged event.
      for (int i=0; i<Count; ++i)
      {
        NumberListItem nli = (NumberListItem) this[i];
        nli.NLValue = 2*i + 1;
      }
    }

    public void SetEven()
    {
      // Each of these NLValue assignments causes a NotifyPropertyChanged event.
      for (int i=0; i<Count; ++i)
      {
        NumberListItem nli = (NumberListItem) this[i];
        nli.NLValue = 2*(i+1);
      }
    }
  }
}
