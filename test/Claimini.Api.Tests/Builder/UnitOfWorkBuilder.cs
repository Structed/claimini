using Claimini.Api.Repository;
using Moq;

namespace Claimini.Api.Tests.Builder
{
    public class UnitOfWorkBuilder
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IUnitOfWork> mock;

        public UnitOfWorkBuilder(MockRepository mockRepository)
        {
            this.mockRepository = mockRepository;
            this.mock = this.mockRepository.Create<IUnitOfWork>();
        }

        public Mock<IUnitOfWork> Build()
        {
            return this.mock;
        }

        public IUnitOfWork BuildObject()
        {
            return this.mock.Object;
        }

        public UnitOfWorkBuilder WithCommitAffectingRows(int affectedRows)
        {
            this.mock.Setup(x => x.Commit()).Returns(affectedRows);

            return this;
        }
    }
}
