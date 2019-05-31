using Android.Graphics;
using MvvmCross.Converters;
using System;
using System.Globalization;

namespace ARDC.BizCard.Droid.Converters
{
    public class ByteArrayToImageValueConverter : MvxValueConverter<byte[], Bitmap>
    {
        protected override Bitmap Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return BitmapFactory.DecodeByteArray(value, 0, value.Length);
        }

        protected override byte[] ConvertBack(Bitmap value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}