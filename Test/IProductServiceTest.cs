using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.SpecificPropertyDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabeledDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
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
        private static ILabeledDao labeledDao;

        private TransactionScope transactionScope;

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
                var product = createProduct(productName, productPrice, productDate, productQuantity, category.categoryId);
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
                var productUpdate = new ProductUpdateDetails(name, price, quantity);

                productService.UpdateProductDetails(product.productId, productUpdate);
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
                productList.Add(createProduct(productName, productPrice, productDate, productQuantity, category.categoryId));
                productList.Add(createProduct("product2", 9, System.DateTime.Now, 10, category.categoryId));
                productList.Add(createProduct("product3", 8, System.DateTime.Now, 13, category.categoryId));

                var productDetailsList = productService.FindAllProducts();

                // Check data
                Assert.AreEqual(productList.Count, productDetailsList.Count);
                for (int i = 0; i < productList.Count; i++)
                {
                    Assert.AreEqual(productList[i].productId, productDetailsList[i].ProductId);
                    Assert.AreEqual(productList[i].productName, productDetailsList[i].ProductName);
                    Assert.AreEqual(productList[i].Category.categoryName, productDetailsList[i].CategoryName);
                    Assert.AreEqual(productList[i].productDate, productDetailsList[i].ProductDate);
                    Assert.AreEqual(productList[i].productPrice, productDetailsList[i].ProductPrice);
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
                productList.Add(createProduct(productName, productPrice, productDate, productQuantity, category.categoryId));
                createProduct("pro2", 9, System.DateTime.Now, 10, category.categoryId);
                productList.Add(createProduct("product3", 8, System.DateTime.Now, 13, category.categoryId));

                var productDetailsList = productService.FindAllProductsByKeyword("product", -1);

                // Check data
                Assert.AreEqual(productList.Count, productDetailsList.Count);
                for (int i = 0; i < productList.Count; i++)
                {
                    Assert.AreEqual(productList[i].productId, productDetailsList[i].ProductId);
                    Assert.AreEqual(productList[i].productName, productDetailsList[i].ProductName);
                    Assert.AreEqual(productList[i].Category.categoryName, productDetailsList[i].CategoryName);
                    Assert.AreEqual(productList[i].productDate, productDetailsList[i].ProductDate);
                    Assert.AreEqual(productList[i].productPrice, productDetailsList[i].ProductPrice);
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
                productList.Add(createProduct(productName, productPrice, productDate, productQuantity, category1.categoryId));
                createProduct("pro2", 9, System.DateTime.Now, 10, category1.categoryId);
                createProduct("product3", 8, System.DateTime.Now, 13, category2.categoryId);

                var productDetailsList = productService.FindAllProductsByKeyword("product", category1.categoryId);

                // Check data
                Assert.AreEqual(productList.Count, productDetailsList.Count);
                for (int i = 0; i < productList.Count; i++)
                {
                    Assert.AreEqual(productList[i].productId, productDetailsList[i].ProductId);
                    Assert.AreEqual(productList[i].productName, productDetailsList[i].ProductName);
                    Assert.AreEqual(productList[i].Category.categoryName, productDetailsList[i].CategoryName);
                    Assert.AreEqual(productList[i].productDate, productDetailsList[i].ProductDate);
                    Assert.AreEqual(productList[i].productPrice, productDetailsList[i].ProductPrice);
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
                var product = createProduct(productName, productPrice, productDate, productQuantity, category.categoryId);

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
                properties.Add(property1);
                specificPropertyDao.Create(property2);
                properties.Add(property2);

                var details = productService.FindProduct(product.productId);

                // Check data
                Assert.AreEqual(product.productName, details.ProductName);
                Assert.AreEqual(product.Category.categoryName, details.CategoryName);
                Assert.AreEqual(product.productPrice, details.ProductPrice);
                Assert.AreEqual(product.productDate, details.ProductDate);
                Assert.AreEqual(product.productQuantity, details.RemainingQuantity);
                Assert.AreEqual(2, details.SpecificName.Count);
                Assert.AreEqual(2, details.SpecificValue.Count);
                for (int i = 0; i < properties.Count; i++)
                {
                    Assert.AreEqual(properties[i].propertyName, details.SpecificName[i]);
                    Assert.AreEqual(properties[i].propertyValue, details.SpecificValue[i]);
                }
            }

        }

        /// <summary>
        /// A test for AddComment.
        /// </summary>
        [TestMethod]
        public void AddCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate, productQuantity, category.categoryId);
                var user = createUser();

                List<long> tagIds = new List<long>();
                tagIds.Add(createTag(tagName1).tagId);
                tagIds.Add(createTag(tagName2).tagId);

                var update = new CommentUpdate(commentBody, tagIds);
                var commentId = productService.AddComment(product.productId, user.usrId, update);

                var findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(product.productId, findComment.productId);
                Assert.AreEqual(user.usrId, findComment.userId);
                Assert.AreEqual(commentBody, findComment.comment1);
                Assert.AreEqual(System.DateTime.Now.Date, findComment.commentDate.Date);
                Assert.AreEqual(2, findComment.Labeleds.Count);

                int i = 0;
                foreach (var l in findComment.Labeleds)
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
                var product = createProduct(productName, productPrice, productDate, productQuantity, category.categoryId);
                var user = createUser();

                List<long> tagIds = new List<long>();
                tagIds.Add(createTag(tagName1).tagId);
                tagIds.Add(createTag(tagName2).tagId);

                var update = new CommentUpdate(commentBody, tagIds);
                var commentId = productService.AddComment(product.productId, user.usrId, update);

                var findComment = commentDao.Find(commentId);
                var labeleds = new List<long>();
                foreach (var l in findComment.Labeleds)
                {
                    labeleds.Add(l.labeledId);
                }

                productService.DeleteComment(commentId);
                commentDao.Find(commentId);

                foreach (var l in labeleds)
                {
                    labeledDao.Find(l);
                }
            }
        }

        /// <summary>
        /// A test for UpdateComment.
        /// </summary>
        [TestMethod]
        public void UpdateCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var category = createCategory(categoryName);
                var product = createProduct(productName, productPrice, productDate, productQuantity, category.categoryId);
                var user = createUser();

                List<long> tagIds = new List<long>();
                tagIds.Add(createTag(tagName1).tagId);
                tagIds.Add(createTag(tagName2).tagId);

                var update = new CommentUpdate(commentBody, new List<long>());
                var commentId = productService.AddComment(product.productId, user.usrId, update);

                var findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(product.productId, findComment.productId);
                Assert.AreEqual(user.usrId, findComment.userId);
                Assert.AreEqual(commentBody, findComment.comment1);
                Assert.AreEqual(System.DateTime.Now.Date, findComment.commentDate.Date);
                Assert.AreEqual(0, findComment.Labeleds.Count);

                update = new CommentUpdate("commentTest2",tagIds);
                productService.UpdateComment(commentId, update);
                findComment = commentDao.Find(commentId);

                // Check data
                Assert.AreEqual(update.CommentBody, findComment.comment1);
                Assert.AreEqual(2, findComment.Labeleds.Count);

                int i = 0;
                foreach (var l in findComment.Labeleds)
                {
                    Assert.AreEqual(tagIds[i], l.tagId);
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
                var product = createProduct("anotherProductNameTest", productPrice, productDate, productQuantity, category.categoryId);
                var user = createUser();

                List<long> tagIds1 = new List<long>();
                List<long> tagIds2 = new List<long>();
                tagIds1.Add(createTag(tagName1).tagId);
                tagIds2.Add(createTag(tagName2).tagId);

                List<string> commentsBody = new List<string>();
                commentsBody.Add(commentBody);
                commentsBody.Add("comment2");
                var update1 = new CommentUpdate(commentsBody[0], tagIds1);
                var update2 = new CommentUpdate(commentsBody[1], tagIds2);

                List<long> commentsIds = new List<long>();
                commentsIds.Add(productService.AddComment(product.productId, user.usrId, update1));
                commentsIds.Add(productService.AddComment(product.productId, user.usrId, update2));

                var listComments = productService.FindAllProductComments(product.productId);

                // Check data
                Assert.AreEqual(commentsIds.Count, listComments.Count);
                for (int i = 0; i < listComments.Count; i++)
                {
                    Assert.AreEqual(commentsIds[i], listComments[i].CommentId);
                    Assert.AreEqual(user.usrId, listComments[i].UserId);
                    Assert.AreEqual(commentsBody[i], listComments[i].CommentBody);
                    Assert.AreEqual(System.DateTime.Now.Date, listComments[i].CommentDate.Date);
                }
                Assert.AreEqual(tagName1, listComments[0].TagNames[0]);
                Assert.AreEqual(tagName2, listComments[1].TagNames[0]);
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

                var tagFoundList = productService.FindAllTags();

                // Check data
                Assert.AreEqual(tagList.Count, tagFoundList.Count);
                for (int i = 0; i < tagList.Count; i++)
                {
                    Assert.AreEqual(tagList[i].tagId, tagFoundList[i].tagId);
                    Assert.AreEqual(tagList[i].tagName, tagFoundList[i].tagName);
                }            
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
            labeledDao = kernel.Get<ILabeledDao>();

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
