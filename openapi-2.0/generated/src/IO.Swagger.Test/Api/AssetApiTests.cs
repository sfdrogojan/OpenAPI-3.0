/* 
 * Marketing Cloud REST API
 *
 * Marketing Cloud's REST API is our newest API. It supports multi-channel use cases, is much more lightweight and easy to use than our SOAP API, and is getting more comprehensive with every release.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: mc_sdk@salesforce.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Dynamic;
using NUnit.Framework;
using IO.Swagger.Api;
using IO.Swagger.Model;
using IO.Swagger.Client;

namespace IO.Swagger.Test
{
    /// <summary>
    ///  Class for testing AssetApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class AssetApiTests : ApiTests
    {
        private AssetApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new AssetApi(
                authBasePath, 
                clientId, 
                clientSecret, 
                accountId);
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }
   
        /// <summary>
        /// Test GetAssetById
        /// </summary>
        [Test]
        public void GetAssetByIdTest()
        {
            decimal? id = 273724;
            var response = instance.GetAssetById(id);
            Assert.IsInstanceOf<Asset> (response, "response is Asset");
        }

        /// <summary>
        /// Test PartiallyUpdateAsset
        /// </summary>
        [Test]
        public void PartiallyUpdateAssetTest()
        {
            decimal? id = 273724;
            Asset asset = instance.GetAssetById(id);
            asset.Description = Guid.NewGuid().ToString();
            var response = instance.PartiallyUpdateAsset(id, asset);
            Assert.IsInstanceOf<Asset>(response, "response is Asset");
        }

        [Test]
        public void CreateAssetTest()
        {
            string customerKey = Guid.NewGuid().ToString();
            string name = $"Automation POC {Guid.NewGuid()}";
            string description = "Automation POC Description";  
            
            var assetType = new AssetType(196, "textblock", "Text Block");
            var asset = new Asset(null, customerKey, null, null, assetType, null, null, null, name, description);

            var response = instance.CreateAsset(asset);

            Assert.IsInstanceOf<Asset>(response);
        }

        [Test]
        public void DeleteAssetTest()
        {
            string customerKey = Guid.NewGuid().ToString();
            string name = $"Automation POC {Guid.NewGuid()}";
            string description = $"Automation POC Description {Guid.NewGuid()}";

            var assetType = new AssetType(196, "textblock", "Text Block");
            var asset = new Asset(null, customerKey, null, null, assetType, null, null, null, name, description);

            var response = instance.CreateAsset(asset);
            Assert.IsInstanceOf<Asset>(response);

            var assetToDeleteId = response.Id;
            instance.DeleteAssetById(assetToDeleteId);

            try
            {
                instance.GetAssetById(assetToDeleteId);
                Assert.Fail("No exception thrown");
            }
            catch (ApiException e)
            {
                Assert.AreEqual(404, e.ErrorCode);
                Assert.AreEqual("Error calling GetAssetById: ", e.Message);
            }
        }
    }
}
