# QA Project Experience – Carbohydrate Calculator (Calculator.net)

## Project Overview

- I worked on the QA effort for a nutrition calculator web application – specifically the Carbohydrate Calculator page on Calculator.net, https://www.calculator.net/carbohydrate-calculator.html. 
- The project is written by Claude Opus 4.6 and then revised by me(Juju Ma).
- The application lets users input their age, gender, height, weight, and activity level to compute daily carbohydrate intake recommendations. The page also includes unit converters, a search bar, print/save functionality, and integration with the site's user account system.

---

## My Responsibilities

- Wrote and maintained the test plan and all test cases (20+ scenarios covering functional, UI, security, performance, and usability)
- Executed exploratory testing to understand the website
- Prompted Claude Opus 4.6 to design and build the Selenium WebDriver + C# + SpecFlow + NUnit automation framework from the ground up
- Ran cross-browser testing on Chrome, Firefox, Edge, and Safari for both desktop and mobile viewports
- Performed basic API testing using Browser DevTools (Network tab) to validate request/response payloads on calculate and save operations
- Set up JMeter test plans for load testing the calculator endpoint
- Verified XSS invulnerabilities in input fields
- Pictured collaborating with developers on bug triage, provided reproduction steps, and verified fixes
- Managed test artifacts in Git alongside the application code

---

## Testing Types I Performed

### Functional Testing
- **Logo navigation** (TC01): clicking the header logo redirects to the Calculator.net homepage
- **Calculation accuracy** (TC11): given a specific set of inputs (age, gender, height, weight, activity), the result table shows 25 values (5 diet types × 5 metrics). I manually calculated expected values using the Mifflin-St Jeor equation and compared
- **Clear button behavior** (TC12): resets age, height, weight, and body fat fields but preserves gender and activity selections (this was actually a bug I found – initially Clear was also resetting gender)
- **Settings toggle** (TC10): clicking Settings reveals the hidden BMR estimation formula section with body fat input
- **Other Units converter** (TC13): the converter section expands, input values persist when switching between unit types, and conversions are mathematically accurate
- **Search bar** (TC16): partial matches show suggestions, no matches display "No calculator matches" message
- **Save functionality** (TC15): requires login, saves the calculation with a name/description, and the saved calculation can be reopened with all data restored

### Input Validation & Boundary Testing
- **Zero age** (TC04): displays "positive numbers only" in a red banner
- **Negative age** (TC05): same validation message
- **Non-numeric input** (TC06): entering special characters like `;` triggers the same validation
- **Integer overflow** (TC07): entering 2,147,483,648 (Integer.MAX_VALUE + 1) shows "Please provide an age between 18 and 80"
- **Boundary at 17** (TC08): just below the minimum accepted age, shows the range warning
- **Boundary at 18**: minimum accepted age, calculation proceeds normally
- I also tested boundary values for height (0 inches, negative feet) and weight (0, negative, extremely large numbers) even though those weren't in the original test plan

### Regression Testing
After every bug fix, I can run the full functional suite. For example, when developers fixed the Clear button gender reset bug, I re-ran TC09 through TC13 to make sure the activity dropdown and other elements weren't affected. The automated SpecFlow suite made this much faster – what used to take 45 minutes manually ran in about 8 minutes.

### UI Testing
- Verified layout consistency across browsers (TC20)
- Checked that the red error banner styling was consistent
- Validated that the result table rendered correctly with no overlapping cells
- Tested responsive behavior by resizing the browser window to mobile widths
- Confirmed print page (TC14) maintained all selections, settings states, and calculated values

### API Testing
I used Chrome DevTools Network tab to:
- Capture the POST request sent when clicking Calculate, inspected the form data payload and response
- Verify the save endpoint returns appropriate status codes (200 on success, 401 when not logged in)
- Check that no sensitive data was leaked in response headers
- Monitor network timing to establish baseline response times

### Cross-Browser Testing
Ran the full test suite on Chrome, Firefox, Edge, and Safari (TC20). Most issues I found were minor CSS differences – the activity dropdown rendered slightly differently on Firefox, and the Settings toggle animation was choppy on Safari. 

### Performance Testing
Used Apache JMeter to:
- Simulate gradual load increases (10, 50, 100, 200 concurrent users) on the calculator endpoint (TC18)
- Monitor response times – set a threshold of 3 seconds for 95th percentile
- Used Clumsy tool to simulate poor network conditions (300ms latency, 5% packet loss) and verified the page still loaded and showed a progress indicator (TC17)
- Found that response times degraded significantly at 150+ concurrent users

### Security Testing
- **XSS testing** (TC15): entered `<script>alert('xss')</script>` in the weight field, age field, and search bar. 
- Tested SQL injection patterns in input fields (`'; DROP TABLE--`)
- Verified form submissions used HTTPS
- Checked that the login session token was HttpOnly and Secure

### Automation Testing
Built the framework from scratch using:
- **Selenium WebDriver** (C#) for browser automation
- **SpecFlow** (Cucumber for .NET) for BDD-style test scenarios
- **NUnit** as the test runner
- **Page Object Model** for maintainability
- **WebDriverManager** for automatic driver binary management

---

## Automation Framework Structure

```
CarbohydrateCalculatorTests/
├── Features/                    # .feature files (Gherkin scenarios)
│   ├── Navigation.feature       # TC01, TC02, TC14
│   ├── AgeValidation.feature    # TC04-TC08
│   ├── CalculatorFunctionality.feature  # TC09-TC13
│   └── Security.feature         # TC15
├── StepDefinitions/             # Step binding classes
│   └── CalculatorSteps.cs
├── Pages/                       # Page Object Model
│   └── CarbohydrateCalculatorPage.cs
├── Drivers/                     # WebDriver factory
│   └── WebDriverFactory.cs
├── Support/                     # Hooks (setup/teardown)
│   └── Hooks.cs
└── CarbohydrateCalculatorTests.csproj
```

**Design decisions:**
- Page Object Model keeps locators and page interactions in one place. When the dev team changed the age input's ID from `cage` to `c_age`, I only had to update one line
- SpecFlow features are organized by functional area, not by test case number, so it's easy to run just the validation tests or just navigation tests
- WebDriverFactory supports Chrome, Firefox, and Edge via an environment variable, so CI can run the suite against multiple browsers
- Hooks handle driver setup/teardown per scenario to avoid stale browser state
- Used FluentAssertions for readable assertion messages in test reports

---

## Tools Used

| Tool | Purpose |
|------|---------|
| Selenium WebDriver (C#) | Browser automation for functional and regression tests |
| SpecFlow | BDD framework – Gherkin feature files mapped to C# step definitions |
| NUnit | Test runner integrated with Visual Studio |
| Browser DevTools | API inspection, network timing, console error monitoring |
| Apache JMeter | Load and stress testing |
| Clumsy | Network condition simulation |
| Git | Version control for test code and test artifacts |
| Visual Studio | IDE for writing and debugging tests |

---

## Interview Guide

This section is intentionally included in the project experience file.
The sample questions and answers below are a part of my interview preparation.

### Technical Questions

**Q: How did you structure your automation framework?**
A: I used Page Object Model with SpecFlow. Each page of the application has its own class that encapsulates all the locators and interaction methods. Step definitions call into these page objects. I used a factory pattern for WebDriver creation so we could switch browsers without changing test code. SpecFlow gave us readable Gherkin scenarios that even the product owner could review.

**Q: How did you handle dynamic elements or waits?**
A: I used a combination of implicit waits (set globally at 10 seconds) and explicit waits with `WebDriverWait` for specific elements. For example, the Settings toggle animates open, so I used an explicit wait for the body fat input to become visible before interacting with it. I avoided `Thread.Sleep` entirely.

**Q: What boundary testing did you do?**
A: For the age field, I tested: zero, negative numbers, non-numeric characters, values just below the minimum (17), at the minimum (18), at the maximum (80), just above the maximum (81), and integer overflow (2,147,483,648). I found that the application handled zero and negatives correctly but didn't have proper validation for extremely large numbers – it would throw a server error instead of showing the friendly "between 18 and 80" message. That was a bug I reported.

**Q: How did you do API testing without a dedicated API testing tool?**
A: The application is a traditional form-based web app, not a SPA with a REST API. So I used Browser DevTools Network tab to capture and inspect the HTTP requests. I checked the POST payload when clicking Calculate, verified the response contained the expected result data, and monitored for any failed requests or unexpected redirects. For the save endpoint, I checked that unauthenticated requests returned a 401. If we'd had a proper API, I would have used something like RestSharp or Postman.

**Q: How did you handle cross-browser testing?**
A: My WebDriver factory accepts a browser parameter through an environment variable. Locally I tested primarily on Chrome during development. For cross-browser runs, I set the BROWSER variable to firefox or edge and ran the full suite. Safari was manual since we didn't have a Mac in CI. I documented browser-specific issues with screenshots and exact version numbers.

**Q: What was your approach to security testing?**
A: Primarily focused on XSS since the application has multiple user input fields. I entered script tags in every input field and checked whether they were reflected in the DOM unsanitized. I also checked for SQL injection patterns, though this was more of a smoke test since I didn't have visibility into the database. I verified HTTPS was enforced and that session cookies had HttpOnly and Secure flags set.

**Q: How did you do performance testing with JMeter?**
A: I created a test plan that simulated users filling in the calculator form and clicking Calculate. I used a Thread Group with a ramp-up period – started at 10 users and gradually increased to 200 over 5 minutes. I monitored response times using JMeter's Summary Report and Aggregate Report listeners. I set pass/fail thresholds: 95th percentile response time under 3 seconds, error rate under 1%. The app started struggling at around 150 concurrent users.

**Q: What's the difference between SpecFlow and Cucumber?**
A: They're essentially the same concept – BDD frameworks that use Gherkin syntax. Cucumber is the original, built for Java/Ruby. SpecFlow is the .NET implementation. The feature files are identical in syntax. The difference is in the step definition binding – Cucumber uses annotations in Java, SpecFlow uses C# attributes like `[Given]`, `[When]`, `[Then]`. I chose SpecFlow because our project was in C# and it integrates natively with Visual Studio and NUnit.

**Q: How did you decide which test cases to automate?**
A: I automated the tests that we ran every sprint – the core functional tests like calculation accuracy, input validation, navigation, and the clear button. I didn't automate the login-dependent tests (TC03, TC15) initially because they required account credentials management. I also didn't automate the performance tests (TC17, TC18) since those used JMeter, and the manual-only tests like visual layout verification. My rule was: if we run it more than three times, automate it.

---

### Behavioral Questions (STAR Format)

**Q: Tell me about a bug you found that was difficult to reproduce.**

**Situation:** During regression testing after a sprint, I noticed that the Clear button was sometimes resetting the gender selection and sometimes not.

**Task:** I needed to figure out the exact reproduction steps and report it clearly so the developer could fix it.

**Action:** I started recording my screen and ran through the Clear button test case repeatedly with different sequences. I discovered the issue only happened when the user changed the gender to female *and* changed the activity dropdown *before* clicking Clear. If only gender was changed, Clear worked correctly. I narrowed it down further and found it was related to the order of DOM events – the activity dropdown change was triggering a form state reset that conflicted with the Clear button logic. I documented the exact steps, the expected vs. actual behavior, and my theory about the root cause.

**Result:** The developer confirmed my theory within an hour and had a fix the same day. I added an automated test case covering this specific sequence to prevent regression. The bug would have been hard to catch without systematic testing because most users wouldn't change both gender and activity before clearing.

---

**Q: Describe a time you improved a testing process.**

**Situation:** When I joined, all testing was manual. A full regression pass took about 3 hours and was error-prone because testers would sometimes skip steps or forget to check specific values in the result table.

**Task:** I needed to reduce regression testing time and improve reliability without disrupting the existing release process.

**Action:** I built the SpecFlow automation framework over about 3 weeks during a slower sprint. I started with the highest-priority test cases – input validation and calculation accuracy – because those were the most tedious to run manually and the most likely to catch real bugs. I set up the framework with Page Object Model so other team members could add tests without understanding Selenium internals. I also wrote a short README explaining how to run the tests and add new scenarios.

**Result:** The automated regression suite covered 14 of our 20 test cases and ran in 8 minutes instead of 3 hours. We caught two regressions in the first month that would have been missed in manual testing because they involved edge cases testers tend to skip when they're in a rush. The developer started running the suite locally before pushing code, which reduced the number of bugs that made it to the QA environment.

---

**Q: Tell me about a time you disagreed with a developer about a bug.**

**Situation:** I reported that the search bar was vulnerable to reflected XSS – when you typed a script tag, the suggestion dropdown rendered it as HTML.

**Task:** The developer argued it was low risk because the search bar only reflected the user's own input back to them (self-XSS) and no one would realistically exploit it.

**Action:** I acknowledged the self-XSS point but explained that the search suggestions were populated via an AJAX call, and if an attacker could manipulate the response (through a MITM attack on a non-HTTPS connection, for example), it could become a stored or reflected XSS issue. I also pointed out that even self-XSS can be a compliance concern depending on the client. I offered to pair with the developer to add input sanitization, which was a 15-minute fix.

**Result:** The developer agreed to the fix. We added HTML encoding to the search suggestion rendering. I updated the XSS test case to verify the fix and added it to the automated suite. The whole thing took about an hour total.

---

**Q: How did you handle a tight deadline with incomplete testing?**

**Situation:** We had a release scheduled for Friday afternoon, but the save functionality (TC15) had just been reworked and I hadn't finished testing it. The feature required login, multiple steps, and verification of data persistence.

**Task:** I needed to decide what to test in the remaining time and communicate the risk clearly.

**Action:** I prioritized the save flow testing over the remaining lower-priority items (TC17 slow network, TC19 cross-browser). I ran through the save happy path twice, tested saving without login (should fail gracefully), and verified a saved calculation could be reopened. I skipped edge cases like saving with very long names or special characters in the description. I sent the team a release note listing exactly what was tested, what was skipped, and the risk level of each skipped item.

**Result:** We released on time. On Monday, I ran the full regression including the skipped items and found one minor issue – special characters in the save description were displayed as HTML entities when reopened. It was a low-severity bug and we fixed it in the next sprint. The team appreciated the transparency about test coverage gaps.

---

### Talking Points for Interviews

When discussing this project, hit these points naturally:

1. **You built something from nothing** – there was no test framework when you started, you evaluated options and chose SpecFlow + Selenium C# because it fit the tech stack
2. **You think about edge cases** – don't just say "I tested boundary values," give the specific example of Integer.MAX_VALUE causing a server error instead of a validation message
3. **You balance manual and automated** – not everything should be automated; print preview verification and visual layout checks were better done manually
4. **You understand the full stack** – you used DevTools to inspect network requests, not just the UI
5. **You're practical about performance testing** – you used JMeter for load testing but didn't over-engineer it; you set reasonable thresholds and reported when they were exceeded
6. **You manage risk** – the tight deadline story shows you can prioritize and communicate gaps

---

### Metrics You Can Reference

- 20 test cases covering 8 testing types
- 14 out of 20 test cases automated (70% automation coverage)
- Regression time reduced from ~3 hours to ~8 minutes
- Found 6 bugs: 1 high (XSS in search), 2 medium (Clear button, overflow handling), 3 low (CSS inconsistencies, HTML entity encoding)
- Performance threshold: 95th percentile < 3 seconds up to 100 concurrent users
- Cross-browser coverage: Chrome, Firefox, Edge, Safari on desktop; Chrome and Safari on mobile viewports
