
using RealEstatePro.Domain.EstateImages;
using RealEstatePro.Domain.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RealEstatePro.Domain.Estates;
public class Estate
{
    [Key]
    public Guid Id { get; private set; }


    [ForeignKey(nameof(User))]
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Estate(Guid id, string name, EstateCategory estateCategory, string address, decimal price, string? description, decimal surfaceArea, int floorNumber, bool isPromoted, DateTime createdOnUtc)
    {
        Id = id;
        Name = name;
        EstateCategory = estateCategory;
        Address = address;
        Price = price;
        Description = description;
        SurfaceArea = surfaceArea;
        FloorNumber = floorNumber;
        IsPromoted = isPromoted;
        CreatedOnUtc = createdOnUtc;
    }
    public string Name { get; private set; }
    public EstateCategory EstateCategory { get; private set; }
    public string Address { get; private set; }
    public decimal Price { get; private set; }
    public string? Description { get; private set; }
    public decimal SurfaceArea { get; private set; }
    public int FloorNumber { get; private set; }

    public bool IsPromoted { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public List<EstateImage> EstateImages { get; private set; } = [];

    public static Estate CreateEstate(CreateEstateDto estateDto)
    {
        var id = Guid.NewGuid();

        var date = DateTime.UtcNow;

        return new Estate(id, estateDto.Name, estateDto.EstateCategory, estateDto.Address,
             estateDto.Price, estateDto.Description, estateDto.SurfaceArea,
             estateDto.FloorNumber, estateDto.IsPromoted, date);

    }


    //  public BoughtEstate BoughtEstate { get; private set; }
}
