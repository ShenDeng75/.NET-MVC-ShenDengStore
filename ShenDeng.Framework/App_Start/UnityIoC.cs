using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using ShenDeng.Framework.Application;
using ShenDeng.Framework.Application.Imp;
using ShenDeng.Framework.App_Start;
using Unity;
using Unity.Mvc5;

namespace ShenDeng.Framework
{
    public static class UnityIoC
    {
        private static IUnityContainer container { get; set; }

        public static void RegisterComponents()
        {
            //ʵ��������
			container = new UnityContainer();
            //���س���
            var assembly = new List<string>();
            assembly.Add("ShenDeng.Framework");
           // assembly.Add("ShenDeng");
            //�������Խ����Զ�ע��
            foreach (var ass in assembly)
            {
                RegisterType(ass);
            }

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));  //?  ���Լ��ӵ�
        }

        public static void RegisterType(string assembly)
        {
            //�õ���������
            var types = Assembly.Load(assembly).GetTypes();
            foreach (var type in types)
            {
                //�õ������Զ�������
                var attributes = type.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute is RegisterToContainerAttribute)
                    {
                        //�õ����нӿ�
                        var Itypes = type.GetInterfaces();
                        //���Ψһ�ӿڣ��������Ψһ�ľͻᱨ��
                        var Itype = Itypes.Single();
                        //ע������
                        container.RegisterType(Itype, type);
                    }
                }
            }
        }
        //ֱ��ͨ���ӿ����ͻ�ȡʵ��
        public static T  Get<T>()
        {
            try
            {
                return container.Resolve<T>();
            }
            catch (Exception e)
            {
                throw new Exception(typeof(T)+"ע������!\n"+e.Message);
            }
            
        }
    }
}