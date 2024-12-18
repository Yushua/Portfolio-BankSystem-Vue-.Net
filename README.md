# Portfolio-BankSystem-Vue-.Net
 Simple Bank system where you can create a Bank account. see you Bank Idea

 first innital Idea. Create account, get money on account, send money over

 this system can be used to verify with other websites. websites like the Booking and Marketplace can connect this this bank system and use that system to send information over.

 there are three types of requests,

 [personal]

 personal requests only need one side to agree, the side sending the money

 [bussinessrequests]

 this is more complicated. a bussiness sends you a request, you turn to a website where you login (securily)
 and accept the request.

 then the bussiness does their thing, they create everything they need to do, and after that, by using the code, FINALISES the request
 only then is the money from the costumer send to the company.

 [request]

 you send a request to someone elses bank, they login, you accept and money is immidietly send. however,
 there is a period one can cancel, and the money is then send back

 banksystem is to relate the security behind a .Net application. making sure several security measures are met. from simple Bearer token, to more pronounce measures.

Authentication and Authorization
    - Identity Frameworks: Use libraries like ASP.NET Identity or Duende IdentityServer to handle user authentication and authorization.
    - OAuth2 and OpenID Connect: Implement modern authentication standards for API security.
    - JWT (JSON Web Tokens): Securely manage session tokens for stateless authentication.
    - Role-based and Policy-based Access Control: Use ASP.NET Core's built-in authorization to define roles and policies for accessing resources.

Data Protection
- Encryption:
    - Encrypt sensitive data at rest using libraries like System.Security.Cryptography.
    - Use SSL/TLS to encrypt data in transit.
Hashing:
- Hash passwords using strong algorithms like PBKDF2, bcrypt, or Argon2.
- Never store plain-text passwords.

nput Validation and Sanitization
- Model Validation: Leverage Data Annotations in ASP.NET to validate input models.
- Avoid SQL Injection:
    - Use parameterized queries or Entity Framework.
    - Avoid building SQL queries using string concatenation.
- Sanitize Inputs: Use libraries like Microsoft.Security.Application.Encoder to sanitize inputs for HTML, JavaScript, or SQL.

API Security
- ate Limiting and Throttling: Prevent abuse by limiting the number of requests using middleware like AspNetCoreRateLimit.
- HMAC or Digital Signatures: Use HMAC for secure message authentication.
- CORS (Cross-Origin Resource Sharing):
    - Configure CORS policies to allow only trusted origins.

Error Handling
- Detailed Error Pages: Disable detailed error messages in production to avoid revealing stack traces.
- Centralized Logging: Use logging frameworks like Serilog or NLog to log errors securely.

Secure Configuration
- Environment Variables: Store sensitive configurations (e.g., connection strings) in environment variables or secure vaults like Azure Key Vault.
- AppSettings.json Security: Use User Secrets in development and encrypt sensitive configurations in production.