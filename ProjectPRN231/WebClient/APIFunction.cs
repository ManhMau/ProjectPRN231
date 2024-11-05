
using BussinessObject.DTOS;
using BussinessObject.DTOS.Common;
using BussinessObject.DTOS.User;


namespace WebClient
{
    public class APIFunction
    {

        private static readonly HttpClient client = new HttpClient();

        private static readonly string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

        public static async Task<APIResponseModel> LoginAsync(LoginVM model)
        {
            string url = "http://localhost:5121/api/Users/Login";


            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<APIResponseModel>();
            }

            return new APIResponseModel { IsSuccess = false };
        }
        public static async Task<UserDTO?> GetUserByUserNameAsync(string userName)
        {
            string url = $"http://localhost:5121/api/Users/GetUserByName/{userName}";

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }

            return null;
        }



        public static async Task<APIResponseModel> RegisterAsync(RegisterVM model)
        {
            string url = "http://localhost:5121/api/Users/Register";


            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<APIResponseModel>();
            }

            return new APIResponseModel { IsSuccess = false };
        }
        public static async Task<List<DocumentDTO>> GetListDocuments()
        {
            string url = "http://localhost:5121/api/Documents/GetAllDocuments";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocumentDTO>>();
            }
            return new List<DocumentDTO>();

        }
        public static async Task<DocumentDTO> GetDocumentById(int id)
        {
            string url = $"http://localhost:5121/api/Documents/GetDocumentById/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DocumentDTO>();
            }
            return new DocumentDTO();

        }

        public static async Task<int> DeleteDocument(int id)
        {
            string url = $"http://localhost:5121/api/Documents/Delete/{id}";
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return 200;
            }
            return -1;
        }

        public static async Task<List<DocTypeMapper>> GetListDocType()
        {
            string url = "http://localhost:5121/api/DocType/GetAllCategories";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocTypeMapper>>();
            }
            return new List<DocTypeMapper>();

        }
        public static async Task<List<DocumentDTO>> SearchDocumentsByTitle(string title)
        {
            string url = $"http://localhost:5121/api/Documents/SearchByTitle?title={Uri.EscapeDataString(title)}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocumentDTO>>();
            }
            return new List<DocumentDTO>();
        }


        public static async Task<List<DocumentDTO>> SortDocumentsByDate(bool descending)
        {
            string url = $"http://localhost:5121/api/Documents/SortByDate?descending={descending}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocumentDTO>>();
            }
            return new List<DocumentDTO>();
        }
        public static async Task<List<UserDTO>> GetAllUsersAsync()
        {
            string url = "http://localhost:5121/api/Users/GetAllUser";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            }

            return new List<UserDTO>();
        }
        public static async Task<UserDTO> GetUserByIdAsync(string id)
        {
            string url = $"http://localhost:5121/api/Users/GetUserById/{id}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }

            return new UserDTO();
        }
        public static async Task<int> UpdateUserAsync(UserDTO user)
        {
            string url = "http://localhost:5121/api/Users/UpdateUser";
            HttpResponseMessage response = await client.PutAsJsonAsync(url, user);

            if (response.IsSuccessStatusCode)
            {
                return 200;  // Success status code
            }

            return -1;  // Failure status code
        }
        public static async Task<int> DeleteUserAsync(string id)
        {
            string url = $"http://localhost:5121/api/Users/Delete/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return 200;  // Success status code
            }

            return -1;  // Failure status code
        }
        public static async Task<UserDTO> GetUserByName(string name)
        {
            string url = $"http://localhost:5121/api/Users/GetUserByName/{name}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }

            return new UserDTO();
        }
        public static async Task<List<UserDTO>> SearchUser(string title)
        {
            string url = $"http://localhost:5121/api/Users/SearchUser?title={Uri.EscapeDataString(title)}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            }
            return new List<UserDTO>();
        }


        public static async Task<List<UserDTO>> SortUserByName(bool descending)
        {
            string url = $"http://localhost:5121/api/Users/SortByName?descending={descending}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            }
            return new List<UserDTO>();
        }


        // 1. Lấy danh sách tất cả GroupMembers
        public static async Task<List<GroupMemberDTO>> GetListGroupMembers()
        {
            string url = "http://localhost:5121/api/GroupMember";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GroupMemberDTO>>();
            }
            return new List<GroupMemberDTO>();
        }

        // 2. Lấy GroupMember theo Id
        public static async Task<GroupMemberDTO> GetGroupMemberById(int id)
        {
            string url = $"http://localhost:5121/api/GroupMember/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GroupMemberDTO>();
            }
            return new GroupMemberDTO();
        }

   


        // 3. Tạo mới GroupMember
        public static async Task<int> CreateGroupMemberAsync(GroupMemberDTO model)
        {
            string url = "http://localhost:5121/api/GroupMember";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
            return (int)response.StatusCode;
        }

        // 4. Cập nhật GroupMember
        public static async Task<int> UpdateGroupMember(GroupMemberDTO model)
        {
            string url = $"http://localhost:5121/api/GroupMember/{model.Id}";
            HttpResponseMessage response = await client.PutAsJsonAsync(url, model);
            return (int)response.StatusCode;
        }

        // 5. Xóa GroupMember
        public static async Task<int> DeleteGroupMember(int id)
        {
            string url = $"http://localhost:5121/api/GroupMember/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            return (int)response.StatusCode;
        }

        // 6. Tìm kiếm GroupMember theo tên nhóm (NameGroup)
        public static async Task<List<GroupMemberDTO>> SearchGroupMembersByName(string nameGroup)
        {
            string url = $"http://localhost:5121/api/GroupMember/SearchByNameGroup?nameGroup={nameGroup}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GroupMemberDTO>>();
            }
            return new List<GroupMemberDTO>();
        }

        // 7. Sắp xếp GroupMembers theo thuộc tính được chỉ định
        public static async Task<List<GroupMemberDTO>> SortGroupMembers(string sortBy, string sortDirection)
        {
            string url = $"http://localhost:5121/api/GroupMember/SortGroupMembers?sortBy={sortBy}&sortDirection={sortDirection}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<GroupMemberDTO>>();
            }
            return new List<GroupMemberDTO>();
        }


        // 1. Lấy danh sách tất cả DocTypes
        public static async Task<List<DocTypeMapper>> GetListDocTypes()
        {
            string url = "http://localhost:5121/api/DocType/GetAllDocTypes";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocTypeMapper>>();
            }
            return new List<DocTypeMapper>();
        }

        // 2. Lấy DocType theo Id
        public static async Task<DocTypeMapper> GetDocTypeById(int id)
        {
            string url = $"http://localhost:5121/api/DocType/GetDocTypeById/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DocTypeMapper>();
            }
            return new DocTypeMapper();
        }

        // 3. Tạo mới DocType
        public static async Task<int> CreateDocTypeAsync(DocTypeMapper model)
        {
            string url = "http://localhost:5121/api/DocType/AddDocType";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
            return (int)response.StatusCode;
        }

        // 4. Cập nhật DocType
        public static async Task<int> UpdateDocType(DocTypeMapper model)
        {
            string url = $"http://localhost:5121/api/DocType/UpdateDocType";
            HttpResponseMessage response = await client.PutAsJsonAsync(url, model);
            return (int)response.StatusCode;
        }

        // 5. Xóa DocType
        public static async Task<int> DeleteDocType(int id)
        {
            string url = $"http://localhost:5121/api/DocType/DeleteDocType/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            return (int)response.StatusCode;
        }

        // 6. Tìm kiếm DocType theo tên (Name)
        public static async Task<List<DocTypeMapper>> SearchDocTypesByName(string searchDocTypeName)
        {
            string url = $"http://localhost:5121/api/DocType?searchDocTypeName={searchDocTypeName}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocTypeMapper>>();
            }
            return new List<DocTypeMapper>();
        }



        // 7. Sắp xếp DocTypes theo tên
        public static async Task<List<DocTypeMapper>> SortDocTypesByName(string sortBy, string sortDirection)
        {
            string url = $"http://localhost:5121/api/DocType/SortByTypeName=sortBy={sortBy}&sortDirection={sortDirection}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocTypeMapper>>();
            }
            return new List<DocTypeMapper>();
        }

        public static async Task<int> CreateDocumentAsync(DocumentAddDTO p)
        {
            string url = "http://localhost:5121/api/Documents/AddDocument";

            // Ensure the client is set up correctly to accept form data
            var content = new MultipartFormDataContent();

            // Add each property of the DocumentAddDTO to the form
            content.Add(new StringContent(p.Title), "Title");
            content.Add(new StringContent(p.Description ?? string.Empty), "Description");
            content.Add(new StringContent(p.TypeId.ToString()), "TypeId");
            content.Add(new StringContent(p.Status.ToString()), "Status");

            // Add the file if it's not null
            if (p.FilePath != null)
            {
                var fileStream = p.FilePath.OpenReadStream();
                content.Add(new StreamContent(fileStream), "FilePath", p.FilePath.FileName);
            }

            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return 200;
            }

            // Read the response content for error messages
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {errorMessage}");
            return -1;
        }

        public static async Task<int> UpdateDocument(int id, DocumentAddDTO document)
        {
            string url = $"http://localhost:5121/api/Documents/UpdateDocument/{id}";

            // Create a MultipartFormDataContent to send the data
            var content = new MultipartFormDataContent
        {
            { new StringContent(document.Title), "Title" },
            { new StringContent(document.Description ?? string.Empty), "Description" },
            { new StringContent(document.TypeId.ToString()), "TypeId" },
            { new StringContent(document.Status.ToString()), "Status" }
        };

            // Add file if it is not null
            if (document.FilePath != null)
            {
                var fileStream = document.FilePath.OpenReadStream();
                content.Add(new StreamContent(fileStream), "FilePath", document.FilePath.FileName);
            }

            // Send a PUT request with MultipartFormDataContent
            try
            {
                var response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return 200; // Success status code
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Update Document Error: {errorMessage}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            return -1; // Failure status code
        }

        // Define the download path

        public static async Task<(int, string)> DownloadDocument(int id) // Trả về tuple (trạng thái, đường dẫn)
        {
            string url = $"http://localhost:5121/api/Documents/Download/{id}";

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var fileStream = await response.Content.ReadAsStreamAsync();
                    var contentDisposition = response.Content.Headers.ContentDisposition;
                    var fileName = contentDisposition?.FileName.Trim('"') ?? $"document_{id}.pdf"; // Tên tệp mặc định

                    // Đường dẫn đến thư mục Downloads
                    string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                    // Đảm bảo thư mục Downloads tồn tại
                    if (!Directory.Exists(downloadPath))
                    {
                        Directory.CreateDirectory(downloadPath);
                    }

                    // Lưu tệp
                    using (var file = new FileStream(Path.Combine(downloadPath, fileName), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await fileStream.CopyToAsync(file);
                    }

                    // Lưu đường dẫn vào biến để trả về
                    string savedFilePath = Path.Combine(downloadPath, fileName);
                    Console.WriteLine($"File will be saved to: {savedFilePath}");

                    return (200, savedFilePath); // Trả về trạng thái và đường dẫn
                }

                Console.WriteLine($"Error downloading document: {await response.Content.ReadAsStringAsync()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            return (-1, null); // Thất bại
        }
        public static async Task<List<DocumentDTO>> GroupDocument()
        {
            string url = "http://localhost:5121/api/Documents/GroupByFileExtension";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocumentDTO>>();
            }
            return new List<DocumentDTO>();
        }


    }
}
