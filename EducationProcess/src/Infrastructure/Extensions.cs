using Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static DateOnly ToDateOnly(this DateTime? datetime)
        {
            return DateOnly.FromDateTime((DateTime)datetime);
        }
    }
}
