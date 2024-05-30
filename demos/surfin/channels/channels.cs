using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;

[assembly: System.Security.AllowPartiallyTrustedCallers()]
namespace Surfin
{
    public class ResourceHelper
    {
        private static System.Windows.Resources.TrustedResourceManager _resourceManager = null;

        internal static ImageData LoadImageResource(string resId)
        {
            IntegerRect sourceRect = new IntegerRect(0, 0, 0, 0);

            if (_resourceManager == null)
                _resourceManager = new System.Windows.Resources.TrustedResourceManager("Surfin.g", 
                        System.Reflection.Assembly.GetEntryAssembly());

            byte[] data = ((byte[])(_resourceManager.GetObject(resId.ToLower())));
            System.IO.Stream stream = new System.IO.MemoryStream(data);

            return new ImageData(stream, null, "image/" + resId.Substring(resId.Length - 3, 3), 
                    PixelFormats.DontCare, false, sourceRect, null, true);
        }
    }

    public class Listings
    {
        string _name;
        ArrayListDataCollection _channels;

        public Listings() : this("") { }
        
        public Listings(string name)
        {
            Name = name;
            _channels = new ArrayListDataCollection();
        }
        
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                    _name = value;
            }
        }
        
        public ArrayListDataCollection Channels
        {
            get { return _channels; }
        }
    }

    public class Channel
    {
        string _name;
        string _title;
        string _source;
        object _tag;
        VideoData _videoData;
        ImageData _imageData;
        
        public Channel() : this("", "", "") { }
        
        public Channel(string name, string title, string source)
        {
            Name = name;
            Title = title;
            Source = source;
        }
        
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                    _name = value;
            }
        }
        
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                    _title = value;
            }
        }
        
        public string Source
        {
            get { return _source; }
            set
            {
                if (_source != value)
                    _source = value.Trim();

                if (_source == "")
                {
                    try
                    {
                        _imageData = ResourceHelper.LoadImageResource("Images/tvglass.jpg");
                    }
                    catch (Exception)
                    {
                        _imageData = null;
                    }
                    _videoData = null;
                }
                else
                {
                    try
                    {
                        _imageData = ResourceHelper.LoadImageResource("Images/" + _source + ".jpg");
                    }
                    catch (Exception)
                    {
                        _imageData = null;
                    }
                    try
                    {
                        _videoData = new VideoData(@"Media\" + _source + ".wmv");
                        _videoData.RepeatCount = 100000;
                        _videoData.Volume = 0;
                        _videoData.Begin = 0;
                    }
                    catch (Exception)
                    {
                        _videoData = null;
                    }
                }
            }
        }
        
        public object Tag
        {
            get { return _tag; }
            set
            {
                if (_tag != value)
                    _tag = value;
            }
        }
        
        public VideoData VideoData
        {
            get { return _videoData; }
        }
        
        public ImageData ImageData
        {
            get { return _imageData; }
        }
        
        public Visibility PurchaseRequired
        {
            get
            {
                Visibility result = Visibility.Collapsed;

                try
                {
                    if ((int)_tag == -1)
                        result = Visibility.Visible;
                }
                catch (Exception) { }
                return result;
            }
        }
    }
}
