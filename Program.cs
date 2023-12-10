using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HomeTask15
{
    public class MyClass
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }
        private double Property3 { get; set; }

        public MyClass() { }

        public MyClass(int property1, string property2)
        {
            Property1 = property1;
            Property2 = property2;
        }

        private MyClass(double property3)
        {
            Property3 = property3;
        }

        public void DisplayProperties()
        {
            Console.WriteLine($"Property1: {Property1}, Property2: {Property2}, Property3: {Property3}");
        }

        private void SetPrivateProperty(double value)
        {
            Property3 = value;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Исследование типа
            Type myClassType = typeof(MyClass);

            // Имя класса
            Console.WriteLine("Имя класса: " + myClassType.Name);

            // Список всех конструкторов
            Console.WriteLine("\nКонструкторы:");
            foreach (ConstructorInfo ctor in myClassType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine(ctor);
            }

            // Список всех полей и свойств
            Console.WriteLine("\nСвойства:");
            foreach (PropertyInfo prop in myClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine(prop);
            }

            // Список всех методов
            Console.WriteLine("\nМетоды:");
            foreach (MethodInfo method in myClassType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine(method);
            }

            // 2. Создание экземпляра
            MyClass myClassInstance = (MyClass)Activator.CreateInstance(myClassType, new object[] { 42, "Проверочная строка" });

            // 3. Манипулирование объектом
            PropertyInfo propInfo1 = myClassType.GetProperty("Свойство1");
            PropertyInfo propInfo2 = myClassType.GetProperty("Свойство2");
            propInfo1.SetValue(myClassInstance, 100);
            propInfo2.SetValue(myClassInstance, "Измененная строка");
            myClassInstance.DisplayProperties();

            // 4. Расширенное исследование
            MethodInfo privateMethod = myClassType.GetMethod("SetPrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic);
            privateMethod.Invoke(myClassInstance, new object[] { 123.456 });
            myClassInstance.DisplayProperties();
        }
    }
}
