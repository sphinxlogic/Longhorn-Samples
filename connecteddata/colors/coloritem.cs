using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media;

namespace Common {

    public class ColorItemList : ArrayListDataCollection
    {
        public ColorItemList() : base(150)
        {
            Type type = typeof(Brushes);
            foreach (PropertyInfo pInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (pInfo.PropertyType == typeof(SolidColorBrush))
                    Add(new ColorItem(pInfo.Name, (SolidColorBrush)pInfo.GetValue(null, null)));
            }
        }
    }

    public class ColorItem : IPropertyChange
    {
        public enum Sources { UserDefined, BuiltIn } ;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ColorItem(string name, SolidColorBrush brush)
        {
            _source = Sources.BuiltIn;
            _name = name;
            _brush = brush;
            Color color = brush.Color;
            _alpha = color.A; _red = color.R; _green = color.G;  _blue = color.B;
            HsvFromRgb();
        }

        public ColorItem(ColorItem item)
        {
            _source = Sources.UserDefined;
            _name = "New Color";
            _red = item._red;   _green = item._green;   _blue = item._blue;
            _hue = item._hue;   _saturation = item._saturation; _value = item._value;
            _alpha = item._alpha;
            _luminance = item._luminance;
            _brush = new SolidColorBrush(item._brush.Color);
        }

        void HsvFromRgb()
        {
            // standard algorithm from nearly any graphics textbook
            int imax = _red, imin = _red;
            if (_green > imax) imax = _green; else if (_green < imin) imin = _green;
            if (_blue > imax) imax = _blue; else if (_blue < imin) imin = _blue;
            double max = imax/255.0, min = imin/255.0;

            double value = max;
            double saturation = (max > 0) ? (max-min)/max : 0.0;
            double hue = _hue;

            if (imax > imin)
            {
                double f = 1.0 / ((max-min)*255.0);
                hue = (imax == _red) ?   0.0 + f*(_green - _blue)
                    : (imax == _green) ? 2.0 + f*(_blue - _red)
                    :                    4.0 + f*(_red - _green);
                hue = hue * 60.0;
                if (hue < 0.0)
                    hue += 360.0;
            }

            // now update the real values as necessary
            if (hue != _hue)
            {
                _hue = hue;
                RaisePropertyChanged("Hue");
            }
            if (saturation != _saturation)
            {
                _saturation = saturation;
                RaisePropertyChanged("Saturation");
            }
            if (value != _value)
            {
                _value = value;
                RaisePropertyChanged("Value");
            }

            UpdateBrush();
        }

        void RgbFromHsv()
        {
            // standard algorithm from nearly any graphics textbook
            double red=0.0, green=0.0, blue=0.0;
            if (_saturation == 0.0)
            {
                red = green = blue = _value;
            }
            else
            {
                double h = _hue;
                while (h >= 360.0)
                    h -= 360.0;

                h = h / 60.0;
                int i = (int)h;

                double f = h - i;
                double r = _value * (1.0 - _saturation);
                double s = _value * (1.0 - _saturation * f);
                double t = _value * (1.0 - _saturation * (1.0 - f));

                switch (i)
                {
                    case 0: red = _value;   green = t;  blue = r;   break;
                    case 1: red = s;   green = _value;  blue = r;   break;
                    case 2: red = r;   green = _value;  blue = t;   break;
                    case 3: red = r;   green = s;  blue = _value;   break;
                    case 4: red = t;   green = r;  blue = _value;   break;
                    case 5: red = _value;   green = r;  blue = s;   break;
                }
            }

            byte iRed = (byte)(red*255.0), iGreen = (byte)(green*255.0), iBlue = (byte)(blue*255.0);
            if (iRed != _red)
            {
                _red = iRed;
                RaisePropertyChanged("Red");
            }
            if (iGreen != _green)
            {
                _green = iGreen;
                RaisePropertyChanged("Green");
            }
            if (iBlue != _blue)
            {
                _blue = iBlue;
                RaisePropertyChanged("Blue");
            }

            UpdateBrush();
        }

        void UpdateBrush()
        {
            Color color = _brush.Color;
            if (_alpha != color.A || _red != color.R || _green != color.G || _blue != color.B)
            {
                color = Color.FromARGB((byte)_alpha, (byte)_red, (byte)_green, (byte)_blue);
                _brush = new SolidColorBrush(color);
                RaisePropertyChanged("Brush");
            }

            double luminance = (0.30*_red + 0.59*_green + 0.11*_blue) / 255.0;
            if (_luminance != luminance)
            {
                _luminance = luminance;
                RaisePropertyChanged("Luminance");
            }
        }

        string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        Sources _source = Sources.UserDefined;
        public Sources Source
        {
            get { return _source; }
        }

        double _luminance;
        public double Luminance
        {
            get { return _luminance; }
        }

        SolidColorBrush _brush;
        public SolidColorBrush Brush
        {
            get { return _brush; }
        }

        byte _alpha;
        public byte Alpha
        {
            get { return _alpha; }
            set { _alpha = value; RaisePropertyChanged("Alpha"); UpdateBrush(); }
        }

        byte _red;
        public byte Red
        {
            get { return _red; }
            set { _red = value; RaisePropertyChanged("Red"); HsvFromRgb(); }
        }

        byte _green;
        public byte Green
        {
            get { return _green; }
            set { _green = value; RaisePropertyChanged("Green"); HsvFromRgb(); }
        }

        byte _blue;
        public byte Blue
        {
            get { return _blue; }
            set { _blue = value; RaisePropertyChanged("Blue"); HsvFromRgb(); }
        }

        double _hue;
        public double Hue
        {
            get { return _hue; }
            set { if (value > 360.0) value = 360.0;  if (value < 0.0) value = 0.0;
                _hue = value; RaisePropertyChanged("Hue"); RgbFromHsv(); }
        }

        double _saturation;
        public double Saturation
        {
            get { return _saturation; }
            set { if (value > 1.0) value = 1.0;  if (value < 0.0) value = 0.0;
                 _saturation = value; RaisePropertyChanged("Saturation"); RgbFromHsv(); }
        }

        double _value;
        public double Value
        {
            get { return _value; }
            set { if (value > 1.0) value = 1.0;  if (value < 0.0) value = 0.0;
                  _value = value; RaisePropertyChanged("Value"); RgbFromHsv(); }
        }
    }
}
