using System.Data.Entity;
using TechmerVision.Models;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace TechmerVision.DAL
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A workspace context. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class WorkspaceContext : DbContext
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public WorkspaceContext() :
            base("WorkspaceContext")
        {
            Database.SetInitializer<WorkspaceContext>(new TechmerVisionDBInitializer());
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the workspaces. </summary>
        ///
        /// <value> The workspaces. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<Workspace> Workspaces { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the grids. </summary>
        ///
        /// <value> The grids. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<Grid> Grids { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the color selections. </summary>
        ///
        /// <value> The color selections. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<ColorSelection> ColorSelections { get; set;}

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     This method is called when the model for a derived context has been initialized, but
        ///     before the model has been locked down and used to initialize the context.  The default
        ///     implementation of this method does nothing, but it can be overridden in a derived class
        ///     such that the model can be further configured before it is locked down.
        /// </summary>
        ///
        /// <remarks>
        ///     Typically, this method is called only once when the first instance of a derived context
        ///     is created.  The model for that context is then cached and is for all further instances
        ///     of the context in the app domain.  This caching can be disabled by setting the
        ///     ModelCaching property on the given ModelBuidler, but note that this can seriously degrade
        ///     performance. More control over caching is provided through use of the DbModelBuilder and
        ///     DbContextFactory classes directly.
        /// </remarks>
        ///
        /// <param name="modelBuilder"> The builder that defines the model for the context being created. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the product templates. </summary>
        ///
        /// <value> The product templates. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<ProductTemplate> ProductTemplates { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a list of colors of the product templates. </summary>
        ///
        /// <value> A list of colors of the product templates. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<ProductTemplateColor> ProductTemplateColors { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the products. </summary>
        ///
        /// <value> The products. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<Product> Products { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a list of colors of the products. </summary>
        ///
        /// <value> A list of colors of the products. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<ProductColor> ProductColors { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the sample inspirations. </summary>
        ///
        /// <value> The sample inspirations. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DbSet<SampleInspiration> SampleInspirations { get; set; }

        public DbSet<Sharing> Sharing { get; set; }
        public DbSet<SampleRequest> SampleRequest { get; set; }
        public DbSet<SampleRequestAsset> SampleRequestAsset { get; set; }
    }
}