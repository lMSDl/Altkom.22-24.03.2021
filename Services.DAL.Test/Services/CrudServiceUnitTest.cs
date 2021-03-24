using Microsoft.EntityFrameworkCore;
using Models;
using Moq;
using Services.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace Services.DAL.Test.Services
{
    public class CrudServiceUnitTest
    {
        [Fact]
        public async Task Get_ResturnsEntityCollection()
        {

            var mocks = new List<Entity>().AsQueryable();

            //Arrage
            var context = new Mock<DbContext>();
            var set = new Mock<DbSet<Entity>>();
            set.As<IQueryable<Entity>>().Setup(x => x.Provider).Returns(new TestAsyncQueryProvider<Entity>(mocks.Provider));
            set.As<IQueryable<Entity>>().Setup(x => x.Expression).Returns(mocks.Expression);
            set.As<IQueryable<Entity>>().Setup(x => x.ElementType).Returns(mocks.ElementType);
            set.As<IQueryable<Entity>>().Setup(x => x.GetEnumerator()).Returns(mocks.GetEnumerator());
            set.As<IAsyncEnumerable<Entity>>().Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<Entity>(mocks.GetEnumerator()));

            context.Setup(x => x.Set<Entity>()).Returns(set.Object);

            var service = new CrudService<Entity>(context.Object);

            //Act
            var result = await service.ReadAsync();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Entity>>(result);
            Assert.Empty(result);
        }

        public static Order DefaultOrder = new Order();

        [Fact]
        public async Task Get_ResturnsEntity_WhenIdExists()
        {
            //Arrage
            var context = new Mock<DbContext>();
            var expected = new Mock<Entity>().Object;
            context.Setup(x => x.Set<Entity>().FindAsync(It.IsAny<int>())).ReturnsAsync(expected);

            var service = new CrudService<Entity>(context.Object);

            //Act
            var result = await service.ReadAsync(default(int));

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Get_ResturnsNull_WhenIdNotExists()
        {
            //Arrage
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<Entity>().FindAsync(It.IsAny<CancellationToken>(), It.IsAny<int>())).ReturnsAsync((Entity)null);

            var service = new CrudService<Entity>(context.Object);

            //Act
            var result = await service.ReadAsync(default(int));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_Resturns_WhenEntityDeleted()
        {
            //Arrage
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<Entity>().FindAsync(It.IsAny<int>())).ReturnsAsync(new Mock<Entity>().Object);

            var service = new CrudService<Entity>(context.Object);

            //Act
            await service.DeleteAsync(default(int));

            //Assert
            context.Verify(x => x.Remove(It.IsAny<Entity>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Returns_WhenEntityNotExists()
        {
            //Arrage
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<Entity>().FindAsync(It.IsAny<CancellationToken>(), It.IsAny<int>()));

            var service = new CrudService<Entity>(context.Object);

            //Act
            await service.DeleteAsync(default(int));

            //Assert
            context.Verify(x => x.Remove(It.IsAny<Entity>()), Times.Never);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public void Delete_ThrowsDbUpdateException_WhenCannotDelete()
        {
            //Arrage
            var context = new Mock<DbContext>();
            var entity = new Mock<Entity>().Object;
            context.Setup(x => x.Set<Entity>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new DbUpdateException());
            var service = new CrudService<Entity>(context.Object);

            //Act
            Func<Task> func = () => service.DeleteAsync(default(int));

            //Assert
            Assert.ThrowsAsync<DbUpdateException>(func);
            context.Verify(x => x.Remove(It.IsAny<Entity>()), Times.Once);
        }

        [Fact]
        public async Task Update_Returns_UpdateSuccessfull()
        {
            //Arrage
            var entity = new Mock<Entity>().Object;
            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<Entity>().Update(It.IsAny<Entity>()));
            var service = new CrudService<Entity>(context.Object);

            //Act
            await service.UpdateAsync(1, entity);

            //Assert
            Assert.Equal(1, entity.Id);
            context.Verify(x => x.Set<Entity>().Update(It.IsAny<Entity>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
