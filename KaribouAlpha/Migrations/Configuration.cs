namespace KaribouAlpha.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KaribouAlpha.DAL.KaribouAlphaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KaribouAlpha.DAL.KaribouAlphaContext context)
        {
            return;

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                if (System.Diagnostics.Debugger.IsAttached == false)
                {
                    System.Diagnostics.Debugger.Launch();
                }
                System.Diagnostics.Debug.WriteLine("Validation Error(s)");

                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
