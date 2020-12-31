using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.SpecificPropertyDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

/// <summary>
/// This is a test class for IProductServiceTest and is intended to contain all IProductServiceTest
/// Unit Tests
/// </summary>
namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class IProductServiceTest
    {

        #region Variables

        private const string categoryName = "category1";

        private const string productName = "product1";
        private const decimal productPrice = 10;
        private System.DateTime productDate = System.DateTime.Now;
        private const int productQuantity = 10;

        private const string commentBody = "comment1";

        private const string tagName1 = "tag1";
        private const string tagName2 = "tag2";

        private static IKernel kernel;
        private static IProductService productService;
        private static ICategoryDao categoryDao;
        private static IProductDao productDao;
        private static ISpecificPropertyDao specificPropertyDao;
        private static ICommentDao commentDao;
        private static ITagDao tagDao;
        private static IUserProfileDao userDao;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion Variables

        private Category createCategory(string categoryName)
        {
            Category category = new Category
            {
                categoryName = categoryName
            };

            categoryDao.Create(category);

            return category;
        }
        private Product createProduct(string productName, decimal productPrice, System.DateTime productDate,
                int productQuantity, long categoryId)
        {

            var product = new Product();
            product.productName = productName;
            product.productPrice = productPrice;
            product.productDate = productDate;
            product.productQuantity = productQuantity;
            product.categoryId = categoryId;

            productDao.Create(product);

            return product;
        }
        private UserProfile createUser()
        {
            UserProfile user = new UserProfile
            {
                loginName = "loginNameTest",
                enPassword = "password",
                firstName = "name",
                lastName = "lastName",
                email = "user@udc.es",
                language = "es",
                country = "ES",
                role = 0,
                address = "addressTest"
            };

            userDao.Create(user);

            return user;
        }
        private Tag createTag(string tagName)
        {
            Tag tag = new Tag
            {
                tagName = tagName
            };

            tagDao.Create(tag);

            return tag;
        }

        /// <summary>
        /// A test for UpdateProduct
        /// </summary>
        [TestMethod]
        public void UpdateProductTest()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate,
                    productQuantity, category.categoryId);
                var productFind = productDao.Find(product.productId);

                // Check data
                Assert.AreEqual(productName, productFind.productName);
                Assert.AreEqual(productPrice, productFind.productPrice);
                Assert.AreEqual(productDate, productFind.productDate);
                Assert.AreEqual(productQuantity, productFind.productQuantity);
                Assert.AreEqual(category.categoryId, productFind.categoryId);

                string name = "update1";
                decimal price = 12;
                int quantity = 13;

                productService.UpdateProductDetails(product.productId, name, price, quantity);
                productFind = productDao.Find(product.productId);

                // Check data
                Assert.AreEqual(name, productFind.productName);
                Assert.AreEqual(price, productFind.productPrice);
                Assert.AreEqual(productDate, productFind.productDate);
                Assert.AreEqual(quantity, productFind.productQuantity);
                Assert.AreEqual(category.categoryId, productFind.categoryId);
            }
        }

        /// <summary>
        /// A test for FindAllProducts
        /// </summary>
        [TestMethod]
        public void FindAllProductsTest()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                List<Product> productList = new List<Product>();
                productList.Add(createProduct(productName, productPrice, productDate,
                    productQuantity, category.categoryId));
                productList.Add(createProduct("product2", 9, System.DateTime.Now, 10, category.categoryId));
                productList.Add(createProduct("product3", 8, System.DateTime.Now, 13, category.categoryId));

                var productDetailsList = productService.FindAllProducts();

                // Check data
                Assert.AreEqual(productList.Count, productDetailsList.Products.Count);

                for (int i = 0; i < productList.Count; i++)
                {
                    Assert.AreEqual(productList[i].productId, productDetailsList.Products[i].productId);
                    Assert.AreEqual(productList[i].productName, productDetailsList.Products[i].productName);
                    Assert.AreEqual(productList[i].Category.categoryName,
                        productDetailsList.Products[i].Category.categoryName);
                    Assert.AreEqual(productList[i].productDate, productDetailsList.Products[i].productDate);
                    Assert.AreEqual(productList[i].productPrice, productDetailsList.Products[i].productPrice);
                }
            }
        }

        /// <summary>
        /// A test for FindAllProductsByKeyword. No Category.
        /// </summary>
        [TestMethod]
        public void FindAllProductsByKeywordTest()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                List<Product> productList = new List<Product>();
                productList.Add(createProduct(productName, productPrice, productDate, productQuantity,
                    category.categoryId));
                createProduct("pro2", 9, System.DateTime.Now, 10, category.categoryId);
                productList.Add(createProduct("product3", 8, System.DateTime.Now, 13, category.categoryId));

                var productDetailsList = productService.FindAllProductsByKeyword("product", -1);

                // Check data
                Assert.AreEqual(productList.Count, productDetailsList.Products.Count);

                for (int i = 0; i < productList.Count; i++)
                {
                    Assert.AreEqual(productList[i].productId, productDetailsList.Products[i].productId);
                    Assert.AreEqual(productList[i].productName, productDetailsList.Products[i].productName);
                    Assert.AreEqual(productList[i].Category.categoryName,
                        productDetailsList.Products[i].Category.categoryName);
                    Assert.AreEqual(productList[i].productDate, productDetailsList.Products[i].productDate);
                    Assert.AreEqual(productList[i].productPrice, productDetailsList.Products[i].productPrice);
                }
            }
        }

        /// <summary>
        /// A test for FindAllProductsByKeyword. With Category.
        /// </summary>
        [TestMethod]
        public void FindAllProductsByKeywordAndCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                var category1 = createCategory(categoryName);
                var category2 = createCategory("category2");

                List<Product> productList = new List<Product>();
                productList.Add(createProduct(productName, productPrice, productDate,
                    productQuantity, category1.categoryId));
                createProduct("pro2", 9, System.DateTime.Now, 10, category1.categoryId);
                createProduct("product3", 8, System.DateTime.Now, 13, category2.categoryId);

                var productDetailsList = productService.FindAllProductsByKeyword("product", category1.categoryId);

                // Check data
                Assert.AreEqual(productList.Count, productDetailsList.Products.Count);
                for (int i = 0; i < productList.Count; i++)
                {
                    Assert.AreEqual(productList[i].productId, productDetailsList.Products[i].productId);
                    Assert.AreEqual(productList[i].productName, productDetailsList.Products[i].productName);
                    Assert.AreEqual(productList[i].Category.categoryName,
                        productDetailsList.Products[i].Category.categoryName);
                    Assert.AreEqual(productList[i].productDate, productDetailsList.Products[i].productDate);
                    Assert.AreEqual(productList[i].productPrice, productDetailsList.Products[i].productPrice);
                }
            }
        }

        /// <summary>
        /// A test for FindProduct.
        /// </summary>
        [TestMethod]
        public void FindProduct()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate,
                    productQuantity, category.categoryId);

                List<SpecificProperty> properties = new List<SpecificProperty>();
                SpecificProperty property1 = new SpecificProperty
                {
                    propertyName = "property1",
                    propertyValue = "value1",
                    productId = product.productId
                };
                SpecificProperty property2 = new SpecificProperty
                {
                    propertyName = "property2",
                    propertyValue = "value2",
                    productId = product.productId
                };

                specificPropertyDao.Create(property1);
                specificPropertyDao.Create(property2);

                Product details = productService.FindProduct(product.productId);

                // Check data
                Assert.AreEqual(product.productName, details.productName);
                Assert.AreEqual(product.Category.categoryName, details.Category.categoryName);
                Assert.AreEqual(product.productPrice, details.productPrice);
                Assert.AreEqual(product.productDate, details.productDate);
                Assert.AreEqual(product.productQuantity, details.productQuantity);
                Assert.AreEqual(2, details.SpecificProperties.Count);

                for (int i = 0; i < properties.Count; i++)
                {
                    Assert.AreEqual(properties[i].propertyName,
                        details.SpecificProperties.ToList()[i].propertyName);
                    Assert.AreEqual(properties[i].propertyValue,
                        details.SpecificProperties.ToList()[i].propertyValue);
                }
            }

        }

        /// <summary>
        /// A test for AddComment within tags.
        /// </summary>
        [TestMethod]
        public void AddComment_WithinTags_est()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate,
                    productQuantity, category.categoryId);
                var user = createUser();

                var commentId = productService.AddComment(product.productId, user.usrId, commentBody);

                var findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(product.productId, findComment.productId);
                Assert.AreEqual(user.usrId, findComment.userId);
                Assert.AreEqual(commentBody, findComment.comment1);
                Assert.AreEqual(System.DateTime.Now.Date, findComment.commentDate.Date);
            }
        }

        /// <summary>
        /// A test for AddComment with tags.
        /// </summary>
        [TestMethod]
        public void AddComment_WithTags_est()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate,
                    productQuantity, category.categoryId);
                var user = createUser();

                List<long> tagIds = new List<long>
                {
                    createTag(tagName1).tagId,
                    createTag(tagName2).tagId
                };

                var commentId = productService.AddComment(product.productId, user.usrId, commentBody, tagIds);

                var findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(product.productId, findComment.productId);
                Assert.AreEqual(user.usrId, findComment.userId);
                Assert.AreEqual(commentBody, findComment.comment1);
                Assert.AreEqual(System.DateTime.Now.Date, findComment.commentDate.Date);
                Assert.AreEqual(2, findComment.Tags.Count);

                int i = 0;

                foreach (var l in findComment.Tags)
                {
                    Assert.AreEqual(tagIds[i], l.tagId);
                    i++;
                }
            }
        }

        /// <summary>
        /// A test for DeleteComment.
        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void DeleteCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate,
                    productQuantity, category.categoryId);
                var user = createUser();

                var commentId = productService.AddComment(product.productId, user.usrId, commentBody);

                var findComment = commentDao.Find(commentId);
                Assert.AreEqual(commentBody, findComment.comment1);

                productService.DeleteComment(commentId);
                commentDao.Find(commentId);
            }
        }

        /// <summary>
        /// A test for UpdateComment adding new Tags.
        /// </summary>
        [TestMethod]
        public void UpdateComment_OnlyAddTags_Test()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate,
                    productQuantity, category.categoryId);
                var user = createUser();

                List<long> tagIds = new List<long>();
                tagIds.Add(createTag(tagName1).tagId);
                tagIds.Add(createTag(tagName2).tagId);

                var commentId = productService.AddComment(product.productId, user.usrId,
                    commentBody, new List<long>());

                var findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(product.productId, findComment.productId);
                Assert.AreEqual(user.usrId, findComment.userId);
                Assert.AreEqual(commentBody, findComment.comment1);
                Assert.AreEqual(System.DateTime.Now.Date, findComment.commentDate.Date);
                Assert.AreEqual(0, findComment.Tags.Count);
                productService.UpdateComment(commentId, "commentTest2", tagIds);
                findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual("commentTest2", findComment.comment1);
                Assert.AreEqual(2, findComment.Tags.Count);

                int i = 0;
                foreach (var l in findComment.Tags)
                {
                    Assert.AreEqual(tagIds[i], l.tagId);
                    i++;
                }
            }
        }

        /// <summary>
        /// A test for UpdateComment adding and delete  Tags.
        /// </summary>
        [TestMethod]
        public void UpdateComment_AddAndRemoveTags_Test()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate,
                    productQuantity, category.categoryId);
                var user = createUser();

                List<long> newTagIds = new List<long>();
                newTagIds.Add(createTag(tagName1).tagId);

                List<long> removeTagIds = new List<long>();
                removeTagIds.Add(createTag(tagName2).tagId);

                var commentId = productService.AddComment(product.productId, user.usrId,
                    commentBody, removeTagIds);

                var findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(product.productId, findComment.productId);
                Assert.AreEqual(user.usrId, findComment.userId);
                Assert.AreEqual(commentBody, findComment.comment1);
                Assert.AreEqual(System.DateTime.Now.Date, findComment.commentDate.Date);
                Assert.AreEqual(1, findComment.Tags.Count);
                int i = 0;
                foreach (var l in findComment.Tags)
                {
                    Assert.AreEqual(removeTagIds[i], l.tagId);
                    i++;
                }

                productService.UpdateComment(commentId, "commentTest2", newTagIds, removeTagIds);
                findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual("commentTest2", findComment.comment1);
                Assert.AreEqual(1, findComment.Tags.Count);

                i = 0;
                foreach (var l in findComment.Tags)
                {
                    Assert.AreEqual(newTagIds[i], l.tagId);
                    i++;
                }
            }
        }

        /// <summary>
        /// A test for FindAllProductComments.
        /// </summary>
        [TestMethod]
        public void FindAllProductCommentsTest()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct("anotherProductNameTest", productPrice,
                    productDate, productQuantity, category.categoryId);
                var user = createUser();

                List<long> tagIds1 = new List<long>();
                List<long> tagIds2 = new List<long>();
                tagIds1.Add(createTag(tagName1).tagId);
                tagIds2.Add(createTag(tagName2).tagId);

                List<string> commentsBody = new List<string>();
                commentsBody.Add(commentBody);
                commentsBody.Add("comment2");

                List<long> commentsIds = new List<long>();
                commentsIds.Add(productService.AddComment(product.productId, user.usrId, commentsBody[0], tagIds1));
                commentsIds.Add(productService.AddComment(product.productId, user.usrId, commentsBody[1], tagIds2));

                CommentBlock listComments = productService.FindAllProductComments(product.productId);

                // Check data
                Assert.AreEqual(commentsIds.Count, listComments.Comments.Count);

                for (int i = 0; i < listComments.Comments.Count; i++)
                {
                    Assert.AreEqual(commentsIds[i], listComments.Comments[i].commentId);
                    Assert.AreEqual(user.usrId, listComments.Comments[i].userId);
                    Assert.AreEqual(commentsBody[i], listComments.Comments[i].comment1);
                    Assert.AreEqual(System.DateTime.Now, listComments.Comments[i].commentDate);
                }

                Assert.AreEqual(tagName1, listComments.Comments[0].Tags.ToList()[0].tagName);
                Assert.AreEqual(tagName2, listComments.Comments[1].Tags.ToList()[0].tagName);
            }
        }

        /// <summary>
        /// A test for AddTag.
        /// </summary>
        [TestMethod]
        public void AddTagTest()
        {
            using (var scope = new TransactionScope())
            {
                var tagId = productService.addTag(tagName1);

                var tag = tagDao.Find(tagId);

                // Check data
                Assert.AreEqual(tagName1, tag.tagName);
            }
        }

        /// <summary>
        /// A test for FindAllTags.
        /// </summary>
        [TestMethod]
        public void FindAllTagsTest()
        {
            using (var scope = new TransactionScope())
            {
                List<Tag> tagList = new List<Tag>();
                tagList.Add(createTag(tagName1));
                tagList.Add(createTag(tagName2));

                TagBlock tagFoundList = productService.FindAllTags();

                // Check data
                Assert.AreEqual(tagList.Count, tagFoundList.Tags.Count);

                for (int i = 0; i < tagList.Count; i++)
                {
                    Assert.AreEqual(tagList[i].tagId, tagFoundList.Tags[i].tagId);
                    Assert.AreEqual(tagList[i].tagName, tagFoundList.Tags[i].tagName);
                }

                Assert.IsFalse(tagFoundList.ExistMoreTags);
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            productDao = kernel.Get<IProductDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            specificPropertyDao = kernel.Get<ISpecificPropertyDao>();
            commentDao = kernel.Get<ICommentDao>();
            tagDao = kernel.Get<ITagDao>();
            userDao = kernel.Get<IUserProfileDao>();

            productService = kernel.Get<IProductService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
        }

        #endregion Additional test attributes
    }

}
