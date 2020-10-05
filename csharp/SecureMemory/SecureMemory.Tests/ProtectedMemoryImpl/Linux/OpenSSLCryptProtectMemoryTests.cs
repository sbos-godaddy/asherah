using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GoDaddy.Asherah.SecureMemory.ProtectedMemoryImpl.Linux;
using Xunit;

namespace GoDaddy.Asherah.SecureMemory.Tests.ProtectedMemoryImpl.Linux
{
    [Collection("Logger Fixture collection")]
    public class OpenSSLCryptProtectMemoryTests : IDisposable
    {
        private readonly LinuxOpenSSL11ProtectedMemoryAllocatorLP64 linuxOpenSSL11ProtectedMemoryAllocatorLP64;

        public OpenSSLCryptProtectMemoryTests()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Debug.WriteLine("\nLinuxOpenSSL11ProtectedMemoryAllocatorTest ctor");
                linuxOpenSSL11ProtectedMemoryAllocatorLP64 = new LinuxOpenSSL11ProtectedMemoryAllocatorLP64(32000, 128);
            }
        }

        public void Dispose()
        {
            linuxOpenSSL11ProtectedMemoryAllocatorLP64.Dispose();
        }

        [Fact]
        private void TestProtectAfterDispose()
        {
            var cryptProtectMemory = new OpenSSLCryptProtectMemory("aes-256-gcm", linuxOpenSSL11ProtectedMemoryAllocatorLP64);
            cryptProtectMemory.Dispose();
            Assert.Throws<Exception>(() => cryptProtectMemory.CryptProtectMemory(IntPtr.Zero, 0));
        }

        [Fact]
        private void TestUnprotectAfterDispose()
        {
            var cryptProtectMemory = new OpenSSLCryptProtectMemory("aes-256-gcm", linuxOpenSSL11ProtectedMemoryAllocatorLP64);
            cryptProtectMemory.Dispose();
            Assert.Throws<Exception>(() => cryptProtectMemory.CryptUnprotectMemory(IntPtr.Zero, 0));
        }
    }
}
