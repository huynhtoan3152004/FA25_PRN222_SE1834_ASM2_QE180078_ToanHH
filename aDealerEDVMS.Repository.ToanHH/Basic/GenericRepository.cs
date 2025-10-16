using aDealerEDVMS.Repository.ToanHH.DBcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aDealerEDVMS.Repository.ToanHH.Basic
//Chỗ này thường sai tên Namespace
//Copy phải cẩn thận
//Khi sử dụng đối tượng này thì sẽ chỉ đích danh. Kế thừa toàn bộ phương thức trong lớp dbcontext,
{
    public class GenericRepository<T> where T : class
    {
        protected FA25_PRN221_SE1834_G5_EVDMSContext _context;

        public GenericRepository()
        {
            _context ??= new FA25_PRN221_SE1834_G5_EVDMSContext();
        }
        //kiểm tra nếu dbcontext null thì khởi tạo mới

        public GenericRepository(FA25_PRN221_SE1834_G5_EVDMSContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        //Kỹ thuật async await , cho phép thực hiện 2 request cùng một lúc, trả về main thread của login, tốc doojo cũng được tối ưu hơn, cùng một công việc so với getall là lấy tất cả các công việc 
        //nhưng không chặn luồng chính, có thể làm việc khác trong khi chờ lấy dữ liệu
        //ToListAsync là phương thức bất đồng bộ, trả về danh sách các phần tử trong tập hợp dưới dạng danh sách
        
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
        //Hàm này để thêm mới một thực thể vào cơ sở dữ liệu đưa vào entity kiểu tree

        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            //// Turning off Tracking for UpdateAsync in Entity Framework
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            //// Turning off Tracking for UpdateAsync in Entity Framework
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(string code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        public T GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        #region Separating asigned entity and save operators        

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}
