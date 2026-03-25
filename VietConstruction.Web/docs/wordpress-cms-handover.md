# Định hướng triển khai WordPress CMS

## 1. Cấu trúc trang

- Trang chủ
- Giới thiệu
- Cơ cấu tổ chức
- Năng lực nhà thầu
- Hồ sơ năng lực
- Hồ sơ pháp lý
- Năng lực tài chính
- Máy móc thiết bị
- Dự án
- Dự án đang thực hiện
- Dự án đã hoàn thành
- Tin tức
- Tin nổi bật
- Thông báo
- Tuyển dụng
- Liên hệ

## 2. Gợi ý cấu hình nội dung trên WordPress

- `Page`: dùng cho toàn bộ trang tĩnh như Giới thiệu, Hồ sơ pháp lý, Năng lực tài chính, Liên hệ.
- `Post`: dùng cho Tin tức, Tin nổi bật, Thông báo, Tuyển dụng nếu đội ngũ muốn quản lý đơn giản nhất bằng post categories.
- `Custom Post Type: du-an`: nên dùng cho nội dung Dự án để dễ quản lý ảnh đại diện, địa điểm, chủ đầu tư, quy mô, trạng thái.
- `Taxonomy: tinh-trang-du-an`: gồm `Đang thực hiện`, `Đã hoàn thành`.
- `Category`: gồm `Tin nổi bật`, `Thông báo`, `Tuyển dụng`.
- `Featured Image`: bắt buộc cho bài dự án, bài tin tức, bài tuyển dụng.

## 3. Vai trò người dùng

- `Administrator`: quản lý toàn bộ nội dung, cài đặt, giao diện, plugin và tạo/sửa/xóa tài khoản Editor.
- `Editor`: tạo, sửa, xóa bài viết và trang nội dung; quản lý chuyên mục, ảnh đại diện và nội dung dự án.

## 4. Trường nội dung nên chuẩn hóa

### Dự án

- Tên dự án
- Ảnh đại diện
- Địa điểm
- Chủ đầu tư
- Quy mô
- Trạng thái dự án
- Mô tả ngắn
- Nội dung chi tiết

### Tuyển dụng

- Vị trí
- Địa điểm làm việc
- Kinh nghiệm
- Mức thu nhập
- Hạn nộp hồ sơ
- Mô tả công việc

## 5. Gợi ý plugin khi triển khai WordPress thực tế

- Form liên hệ: `Contact Form 7` hoặc `WPForms`
- SEO: `Yoast SEO` hoặc `Rank Math`
- Tối ưu hiệu năng: plugin cache phù hợp hạ tầng host
- Sao lưu và bảo mật: chọn theo chính sách vận hành của doanh nghiệp

## 6. Gợi ý dashboard cho người không chuyên kỹ thuật

- Ẩn bớt widget mặc định không cần thiết
- Giữ lại lối vào nhanh cho:
  - Trang tĩnh
  - Dự án
  - Tin tức
  - Tuyển dụng
  - Thư viện ảnh
- Chuẩn hóa hướng dẫn ảnh đại diện theo kích thước đồng nhất cho từng loại nội dung
