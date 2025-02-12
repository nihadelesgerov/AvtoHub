After registration in the AvtoHub project, users can click on the cars on the site and communicate with the advertiser.
A registered user also has the permission to share an ad
The site has security measures provided by Microsoft against attacks such as SQL INJECTION (avoid by EF CORE), CSRF, XSS and Brute Force. 
It's required from User to input lowercase letters, uppercase letters, numbers and symbols (These measures also aim to increase safety)
Open AI Chat GPT is used in design
Thanks for interesting in . Best regards Nihad Alasgarov

UPDATE
        UPDATE 
                UPDATE
                       UPDATE
After spending more time focusing on how things work at background, I learned a lot of things and already know I didn't make an application
I just wrote something that works without thinking about "What's right way to do that?"
All configuration files are secret with Secret Manager tool of ASP.NET CORE
Except AvtoHub Controller, I applied SOLID principles to other both controller, used dependecy Injection,changed authorization policy from Role based to Claim based becase I already know how procces works in the background
Added some logging to expose Warnings and Errors(to console But I know this is not suitable for production)
+ Updated some status code pages middlewares
I learned how to use Layouts to lose coupled codes but it's late ( I have to change all Front-End for not exploding while using sections and layouts)
I added Authorization Handler while using Claim Based Auth.
It's server rendered app that return HTML that's why didn't use JWT for auth

