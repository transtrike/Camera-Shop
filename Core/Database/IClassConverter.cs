using System.Threading.Tasks;

namespace Camera_Shop.Database
{
	public interface IClassConverter<T, TDto> 
		where T   : class
		where TDto : class
	{
		Task<T> DtoToClassAsync(int? cameraId, TDto cameraDTO);
		Task<TDto> ClassToDtoAsync(T camera);
	}
}