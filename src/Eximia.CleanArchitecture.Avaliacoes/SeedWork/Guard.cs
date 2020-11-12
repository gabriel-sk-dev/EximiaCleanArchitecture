using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace Eximia.CleanArchitecture.SeedWork
{
    public static class Guard
    {
        public static Result MustNotBeNullOrEmpty(this string valor, in string motivo)
        {
            return string.IsNullOrEmpty(valor)
                ? Result.Failure(motivo) 
                : Result.Success();
        }

        public static Result MustBeGratherThan(this int valor, in int desejado, in string motivo)
        {
            return valor <= desejado
                ? Result.Failure(motivo)
                : Result.Success();
        }

        public static Result MustBeGratherThan(this decimal valor, in int desejado, in string motivo)
        {
            return valor <= desejado
                ? Result.Failure(motivo)
                : Result.Success();
        }

        public static Result MustBeGratherThanZero(this int valor, in string motivo)
            => valor.MustBeGratherThan(0, motivo);

        public static Result MustBeGratherThanZero(this decimal valor, in string motivo)
            => valor.MustBeGratherThan(0, motivo);

        public static Result MustNotBeEmpty<T>(this IEnumerable<T> valor, string motivo)
        {
            return !valor.Any()
                ? Result.Failure(motivo)
                : Result.Success();
        }

        public static Result MustBe<T>(this T valor, in T desejado, in string motivo) where T : struct
        {
            return !valor.Equals(desejado) 
                ? Result.Failure(motivo)
                : Result.Success();
        }

        public static Result MustBeAll<T>(this IEnumerable<T> valor, in Func<T, bool> condicao, string motivo)
        {
            return !valor.All(condicao)
                ? Result.Failure(motivo)
                : Result.Success();
        }

        public static Result MustBeAny<T>(this IEnumerable<T> valor, in Func<T, bool> condicao, string motivo)
        {
            return !valor.Any(condicao)
                ? Result.Failure(motivo)
                : Result.Success();
        }
    }
}