// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace backend.Application.Interfaces.Services;

public interface IFileStorageService
{
    Task<string> SaveFile(string container, IFormFile file);
    Task<string> EditFile(string container, IFormFile file, string route);
    Task RemoveFile(string route, string container);
}