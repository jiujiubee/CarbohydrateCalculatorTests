**Section** **Details**


Scope Carbohydrate calculator page – functional correctness, UI, validation


Objectives Ensure accurate calculation, error handling, responsiveness, security


Functional, UI, Performance, Integration, Usability, Database, Security, User Acceptance

Test Types


Approach Manual execution; automation (Selenium Java)


Entry Criteria Stable and accessable website


Exit Criteria All high-priority test cases passed


Tools Browser DevTools, Selenium, Apache JMeter, Clumsy https://jagt.github.io/clumsy/index.html


Environment Chrome, Firefox, Edge, Safari; Desktop + Mobile


Risks Calculator formula changes, server down, missing translation


**ID** **Title** **Category** **Steps** **Expected Result** **Priority** **Automated** **Reference(Jira Ticket Number)**



TC01 Calculator Logo Functional


TC02 Login Integration


TC03 Login Response Functional


TC04 Zero Input for Age Functional


TC05 Negative Input for Age Functional


TC06 Non-numeric input for Age Functional


TC07 Integer.MAX_VALUE for Age Functional


TC08 Min Boundary for Age Functional



1. Navigate to the calculator page
2. Click the Calculator.net logo in the header Redirects to https://www.calculator.net/carbohydrate-calculator.html High N N/A


1. Navigate to the calculator page
2. Click the sign in text in the header

Redirects to https://www.calculator.net/my-account/sign-in.php High N N/A


1. Complete login on the login page
2. Navigate back to the calculator page Verify username, "my account" and "sign out" replaces the "sign in" text High N N/A


1. Locate the Age textbox
2. Input 0 Verify a red banner displays "positive numbers only" High N N/A


1. Locate the Age textbox
2. Input a negative number Verify a red banner displays "positive numbers only" Medium N N/A


1. Locate the Age textbox
2. Input ';' Verify a red banner displays "positive numbers only" Medium N N/A


1. Locate the Age textbox
2. Input 2,147,483,648
3. Click Calculate Verify a warning message displays "Please provide an age between 18 and 80." Medium N N/A


1. Locate the Age textbox
2. Input 17
3. Click Calculate Verify a warning message displays "Please provide an age between 18 and 80." High N N/A



TC09 Activity Dropdown List Functional 1. Locate and click the Activity dropdown list Verify the default selection is on Moderate and all 7 choices are available High N N/A


TC10 Hidden Settings Functional 1. Locate the Settings link Verify the hidden BMR estimation formula section is shown High N N/A



TC11 Calculation Result Functional


TC12 Clear Button Functional


TC13 Other Units Functional



1. Input a set of data
2. Click Calculate Verify a table of data is shown in the Result section and all 5x5 values are accurate High N N/A



1. Change the Gender to female
2. Change the Activity selection
3. Click the Clear button


1. Click the Other Units header
2. Enter a number in the From textbox
3. Click through each converter header



TC14 Print Functional 1. Click the Print link



Verify Age, Height, Weight, Settings->Body Fat textboxes are blanked.
Verify Gender and Activity selection are not changed. High N N/A


Verify the convert section is shown after clicking Other Units
Verify the input "From" value persists when switching units
Verify the calculations are accurate High N N/A


Verify the page navigates to a print page by interpreting URL
Verify selections, settings states, values are all maintained High N N/A


Verify the save page displays a success message
Verify the calculation is saved to the logged in account
Verify the saved calculation can be opened and the data in step 2 are restored High N N/A


Verify the matches are shown where there is a match
Verify "No calculator matches "BBBBBB"." is shown after step 3
(Verify the Search button does not do anything) High N N/A



TC15 Save Functional


TC16 Search Bar Functional


TC17 Slow Network Performance



1. Log in
2. Fill in some data
3. Click Calculate
4. Click the Save button
5. Enter some Name and Description on the new page
6. Click Save


1. Locate the search bar
2. Enter 'B'
3. Enter 'BBBBB'



1. Use the Clumsy tool to create network lag
2. Navigate to the page
3. Click Calculate Verify the page is still usable or shows progress Low N N/A



TC18 Stress Test Performance 1. Use JMeter to gradually increase user load



Montior the the resource usage and response time
Verify they fall below a certain threshold Low N N/A



TC19 Compatibility Usability Test on Chrome/Edge/Firefox/Safari Consistent results Medium N N/A



TC20 Security: XSS via input fields Security



1. Locate the Weight textbox
2. Enter some <script> in input The script is not executed High N N/A


