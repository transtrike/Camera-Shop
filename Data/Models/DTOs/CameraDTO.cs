namespace Data.Models.DTOs
{
	public class CameraDTO
	{
		public int Id { get; set; }
		
		public string Brand { get; set; }
		
		public string Model { get; set; }

		public decimal Megapixels { get; set; }

		public int BaseISO { get; set; }
		
		public int MaxISO { get; set; }
	}
}