using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using Web_News.Models;

namespace Web_News.Areas.Admin.ServiceAd.AdvertisementSV
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment; // Thêm vào

        public AdvertisementService(AppDbContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment; // Khởi tạo biến trường
        }

        // Lấy danh sách quảng cáo
        public async Task<List<Advertisement>> GetAdvertisementsAsync()
        {
            return await _context.Advertisements
                .Where(a => a.ApprovalStatus == ApprovalStatus.Pending)
                .ToListAsync();
        }
        public async Task<List<Advertisement>> GetAdvertisementsByStatusAsync(ApprovalStatus? status)
        {
            var query = _context.Advertisements.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(a => a.ApprovalStatus == status);
            }

            return await query.ToListAsync();
        }

        // Lấy chi tiết quảng cáo theo ID
        public async Task<Advertisement?> GetAdvertisementByIdAsync(int id)
        {
            return await _context.Advertisements.FirstOrDefaultAsync(a => a.AdvertisementId == id);
        }

        public async Task ApproveAdvertisementAsync(int advertisementId)
        {
           
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("User ID claim is missing. Ensure the user is logged in.");
            }

            var userId = int.Parse(userIdClaim);

            // Tìm quảng cáo theo ID
            var advertisement = await _context.Advertisements.FindAsync(advertisementId);

            if (advertisement == null)
            {
                throw new Exception("Advertisement not found.");
            }

            // Cập nhật trạng thái và người duyệt
            advertisement.Status = Status.Displayed;
            advertisement.ApprovalStatus = ApprovalStatus.Approved;
            advertisement.ApprovedByUserId = userId;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Advertisements.Update(advertisement);
            await _context.SaveChangesAsync();
        }


        public async Task RejectAdvertisementAsync(int advertisementId)
        {
            // Lấy thông tin người dùng hiện tại từ phiên đăng nhập
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("User ID claim is missing. Ensure the user is logged in.");
            }

            var userId = int.Parse(userIdClaim);

            // Tìm quảng cáo theo ID
            var advertisement = await _context.Advertisements.FindAsync(advertisementId);

            if (advertisement == null)
            {
                throw new Exception("Advertisement not found.");
            }

            // Cập nhật trạng thái và người từ chối
            advertisement.ApprovalStatus = ApprovalStatus.Rejected;
            advertisement.ApprovedByUserId = userId; // Lưu ID người từ chối

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Advertisements.Update(advertisement);
            await _context.SaveChangesAsync();
        }


        public async Task SaveAdvertisementImageAsync(int advertisementId, IFormFile mediaFile)
        {
            var advertisement = await _context.Advertisements.FindAsync(advertisementId);

            if (advertisement == null)
            {
                throw new Exception("Advertisement not found.");
            }

            if (mediaFile != null && mediaFile.Length > 0)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(advertisement.Image))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesAdvertisement", advertisement.Image);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Tạo tên file mới và lưu vào thư mục
                var fileName = Path.GetFileName(mediaFile.FileName);
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesAdvertisement");
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await mediaFile.CopyToAsync(stream);
                }

                advertisement.Image = fileName; // Cập nhật tên file vào mô hình
                _context.Advertisements.Update(advertisement);
                await _context.SaveChangesAsync();
            }
        }
    

    }
}
