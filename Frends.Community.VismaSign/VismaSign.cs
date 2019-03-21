using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

#pragma warning disable 1591

namespace Frends.Community.VismaSign
{

    // Original code: https://gitlab.com/vismasign/vismasign-api-examples/blob/master/v1/csharp/VismaSign.cs
    public class VismaSign
    {
        private static async Task<HttpRequestMessage> WithAuthHeaders(HttpRequestMessage request, ConnectionOption options)
        {
            var macHash = new HMACSHA512(Convert.FromBase64String(options.Secret));
            var contentHash = new MD5CryptoServiceProvider();

            if (request.Content != null)
            {
                request.Content.Headers.ContentMD5 = contentHash.ComputeHash(await request.Content.ReadAsByteArrayAsync());
            }

            request.Headers.Date = request.Headers.Date.GetValueOrDefault(DateTime.UtcNow);

            request.Headers.Authorization =
               new AuthenticationHeaderValue(
                   "Onnistuu",
                   options.Identifier + ":" +
                    Convert.ToBase64String(
                        macHash.ComputeHash(
                            System.Text.Encoding.UTF8.GetBytes(
                                String.Join(
                                    "\n",
                                    new string[] {
                                        request.Method.ToString(),
                                        Convert.ToBase64String(
                                            request.Content != null ?
                                            request.Content.Headers.ContentMD5 :
                                            contentHash.ComputeHash(new byte[] {})
                                        ),
                                        request.Content != null ? request.Content.Headers.ContentType.ToString() : "",
                                        request.Headers.Date.GetValueOrDefault(DateTime.UtcNow).ToString("r"),
                                        request.RequestUri.ToString().Replace(options.BaseAddress, "")
                                    }
                                )
                            )
                        )
                    )
                );

            return request;
        }

        /// <summary>
        /// Create document. See https://sign.visma.net/api/docs/v1/#action-document-create
        /// </summary>
        /// <returns>Object with the following properties: String Location. Dictionary(string,string) Headers. int StatusCode</returns>

        public static async Task<object> DocumentCreate([PropertyTab] DocumentCreateInput input, [PropertyTab] ConnectionOption options, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                options.BaseAddress + "/api/v1/document/"
            );

            request.Content = new StringContent(input.Body, System.Text.Encoding.UTF8, "application/json");
            request = await WithAuthHeaders(request, options);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode && options.ThrowExceptionOnErrorResponse)
                {
                    throw new WebException($"Request to '{request.RequestUri}' failed with status code {(int)response.StatusCode}. Response body: {response.Content}");
                }

                var returnResponse = new HttpResponse
                {
                    Location = response.Headers.Location?.ToString(),
                    StatusCode = (int)response.StatusCode,
                    Headers = GetResponseHeaderDictionary(response.Headers, response.Content.Headers)
                };

                return returnResponse;
            }
        }

        /// <summary>
        /// Add file. See https://sign.visma.net/api/docs/v1/#action-document-add-file
        /// </summary>
        /// <returns>Object with the following properties: String Body. Dictionary(string,string) Headers. int StatusCode</returns>

        public static async Task<object> DocumentAddFile([PropertyTab] DocumentAddFileInput input, [PropertyTab] ConnectionOption options, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                options.BaseAddress + "/api/v1/document/" + input.DocumentUriId + "/files" +
                (
                    input.FileName != null ? "?filename=" + HttpUtility.UrlEncode(input.FileName) : ""
                )
            );

            var requestContent = input.ReadFromFile ? new ByteArrayContent(File.ReadAllBytes(input.FileLocation)) : new ByteArrayContent(input.InputBytes);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            request.Content = requestContent;
            request = await WithAuthHeaders(request, options);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request, cancellationToken);
                var returnResponse = new HttpResponseWithBody
                {
                    Body = await response.Content.ReadAsStringAsync(),
                    StatusCode = (int)response.StatusCode,
                    Headers = GetResponseHeaderDictionary(response.Headers, response.Content.Headers)
                };

                if (!response.IsSuccessStatusCode && options.ThrowExceptionOnErrorResponse)
                {
                    throw new WebException($"Request to '{request.RequestUri}' failed with status code {(int)response.StatusCode}. Response body: {returnResponse.Body}");
                }

                return returnResponse;
            }
        }

        /// <summary>
        /// Add invitation. See https://sign.visma.net/api/docs/v1/#action-document-create-invitations
        /// </summary>
        /// <returns>Object with the following properties: String Body. Dictionary(string,string) Headers. int StatusCode</returns>

        public static async Task<object> DocumentAddInvitations([PropertyTab] DocumentAddInvitationsInput input, [PropertyTab] ConnectionOption options, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                options.BaseAddress + "/api/v1/document/" + input.DocumentUriId + "/invitations"
            );

            request.Content = new StringContent(input.Body, System.Text.Encoding.UTF8, "application/json");
            request = await WithAuthHeaders(request, options);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request, cancellationToken);

                var returnResponse = new HttpResponseWithBody
                {
                    Body = await response.Content.ReadAsStringAsync(),
                    StatusCode = (int)response.StatusCode,
                    Headers = GetResponseHeaderDictionary(response.Headers, response.Content.Headers)
                };

                if (!response.IsSuccessStatusCode && options.ThrowExceptionOnErrorResponse)
                {
                    throw new WebException($"Request to '{request.RequestUri}' failed with status code {(int)response.StatusCode}. Response body: {returnResponse.Body}");
                }

                return returnResponse;
            }
        }

        /// <summary>
        /// Search document. See https://sign.visma.net/api/docs/v1/#search-documents
        /// </summary>
        /// <returns>Object with the following properties: String Body. Dictionary(string,string) Headers. int StatusCode</returns>

        public static async Task<object> DocumentSearch([PropertyTab] DocumentSearchInput query, [PropertyTab] ConnectionOption options, CancellationToken cancellationToken)
        {
            var request = await WithAuthHeaders(new HttpRequestMessage(
                    HttpMethod.Get,
                    options.BaseAddress + "/api/v1/document/" + (query.Query != null ? "?" + query.Query : ""))
                , options);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request, cancellationToken);

                var returnResponse = new HttpResponseWithBody
                {
                    Body = await response.Content.ReadAsStringAsync(),
                    StatusCode = (int)response.StatusCode,
                    Headers = GetResponseHeaderDictionary(response.Headers, response.Content.Headers)
                };

                if (!response.IsSuccessStatusCode && options.ThrowExceptionOnErrorResponse)
                {
                    throw new WebException($"Request to '{request.RequestUri}' failed with status code {(int)response.StatusCode}. Response body: {returnResponse.Body}");
                }

                return returnResponse;
            }
        }

        /// <summary>
        /// Get document using invitation. Use invitation uuid passphrase as parameters. See https://sign.visma.net/api/docs/v1/#action-document-get-file
        /// </summary>
        /// <returns>Object with the following properties: Byte[] Body. Dictionary(string,string) Headers. int StatusCode</returns>
        public static async Task<object> DocumentGet([PropertyTab] DocumentGetInput input, [PropertyTab] ConnectionOption options, CancellationToken cancellationToken)
        {
            var request = await WithAuthHeaders(new HttpRequestMessage(
                    HttpMethod.Get,
                    options.BaseAddress + "/api/v1/invitation/" + input.DocumentUriId + "/" + input.Passphrase + "/files/0"),
                options);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request, cancellationToken);

                var returnResponse = new HttpResponseWithByteArrayBody
                {
                    Body = await response.Content.ReadAsByteArrayAsync(),
                    StatusCode = (int)response.StatusCode,
                    Headers = GetResponseHeaderDictionary(response.Headers, response.Content.Headers)
                };

                if (!response.IsSuccessStatusCode && options.ThrowExceptionOnErrorResponse)
                {
                    throw new WebException($"Request to '{request.RequestUri}' failed with status code {(int)response.StatusCode}. Response body: {returnResponse.Body}");
                }

                return returnResponse;
            }
        }

        private static Dictionary<string, string> GetResponseHeaderDictionary(HttpResponseHeaders responseMessageHeaders, HttpContentHeaders contentHeaders)
        {
            var responseHeaders = responseMessageHeaders.ToDictionary(h => h.Key, h => string.Join(";", h.Value));
            var allHeaders = contentHeaders.ToDictionary(h => h.Key, h => string.Join(";", h.Value));
            responseHeaders.ToList().ForEach(x => allHeaders[x.Key] = x.Value);
            return allHeaders;
        }
    }
}
