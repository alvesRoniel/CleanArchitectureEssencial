namespace CleanArchMvc.Application.ProductsCQRS.Commands
{
    public class ProductUpdateCommand : ProductCommand
    {
        public int Id { get; set; }
    }
}
