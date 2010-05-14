namespace WhoCanHelpMe.Infrastructure.NHibernate
{
    using System;

    public class NHibernateInitializer
    {
        private static readonly object syncLock = new object();
        private static NHibernateInitializer instance;

        protected NHibernateInitializer()
        {
        }

        public bool NHibernateSessionIsLoaded
        {
            get; 
            set;
        }

        public static NHibernateInitializer Instance()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new NHibernateInitializer { NHibernateSessionIsLoaded = false };
                    }
                }
            }

            return instance;
        }

        public void Initialize(Action initMethod)
        {
            if (!this.NHibernateSessionIsLoaded)
            {
                lock (syncLock)
                {
                    if (!this.NHibernateSessionIsLoaded)
                    {
                        initMethod();
                        this.NHibernateSessionIsLoaded = true;
                    }
                }
            }
        }
    }
}