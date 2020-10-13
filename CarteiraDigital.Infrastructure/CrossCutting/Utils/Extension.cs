using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Infrastructure.CrossCutting.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Infrastructure
{
    public static class Extension
    {
        public static string RemoveMaskCpf(this string cpf)
            => cpf.Replace(".", "").Replace("-", "");

        public static string InsertMaskCpf(this string cpf)
            => Convert.ToUInt64(cpf.RemoveMaskCpf()).ToString(@"000\.000\.000\-00");

        public static decimal GetPercentage(this decimal amount, decimal percent)
            => (percent / 100) * amount;

        public static string ConcatMessages(this List<string> words) => string.Join(" ", words);

        public static IResult<T> LoggerError<T>(this IResult<T> result) where T : class
        {
            LogHelper.Error(result.Messages.ConcatMessages());
            return result;
        }

    }
}
