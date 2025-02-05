using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public interface IDiscountService
{
    Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponsAsync();
    Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto);
    Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto);
    Task DeleteDiscountCouponAsync(int couponId);
    Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int couponId);
    Task<ResultDiscountCouponDto> GetCodeDetailByCodeAsync(string code);
    int GetDiscountCouponCountRate(string code);
}

