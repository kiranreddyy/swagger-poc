namespace Swagger.POC.Models
{
    using System;
    using System.Collections.Generic;

    public class SwaggerResponseExamplesAttribute : Attribute
    {
        public SwaggerResponseExamplesAttribute(Type responseType, Type examplesType)
        {
            ResponseType = responseType;
            ExamplesType = examplesType;
        }

        public Type ResponseType { get; set; }
        public Type ExamplesType { get; set; }
    }

    public interface IProvideExamples
    {
        object GetExamples();
    }

    public class UserExamples : IProvideExamples
    {
        public object GetExamples()
        {
            return new List<User>
            {
               new User() { Id=12341,Name="sk1",Address="Hyd1"},
                new User() { Id=12342,Name="sk2",Address="Hyd2"},
            };
        }
    }
}