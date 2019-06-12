using Android.Graphics;
using MvvmCross.Converters;
using System;
using System.Globalization;

namespace ARDC.BizCard.Droid.Converters
{
    /// <summary>
    /// Converter entre Arrays de Bytes e BitMaps.
    /// </summary>
    public class ByteArrayToImageValueConverter : MvxValueConverter<byte[], Bitmap>
    {
        /// <summary>
        /// Converte um array de bytes para um Bitmap.
        /// </summary>
        /// <param name="value">O conjunto de bytes a ser convertido</param>
        /// <param name="targetType">O tipo a ser gerado</param>
        /// <param name="parameter">Parâmetro para a conversão</param>
        /// <param name="culture">Cultura para a localização</param>
        /// <returns>O Bitmap resultante da conversão</returns>
        protected override Bitmap Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return BitmapFactory.DecodeByteArray(value, 0, value.Length);
        }

        /// <summary>
        /// Converte um bitmap para um array de Bytes.
        /// </summary>
        /// <param name="value">O conjunto de bytes a ser convertido</param>
        /// <param name="targetType">O tipo a ser gerado</param>
        /// <param name="parameter">Parâmetro para a conversão</param>
        /// <param name="culture">Cultura para a localização</param>
        /// <returns>Um array de bytes contendo os dados da imagem</returns>
        /// <exception cref="NotSupportedException">Exception lançada quando a operação não é suportada</exception>
        protected override byte[] ConvertBack(Bitmap value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}