namespace ToolSite.Core;

// Full content for launch-ready tools. Registry entries with the same slug are overridden by this.
public static class ToolContent
{
    public static readonly Dictionary<string, ((string H, string P)[] Sections, FaqItem[] Faqs)> Overrides = new()
    {
        ["split-pdf"] = (new (string, string)[]
        {
            ("How to split PDF pages into a new file", "When you need to split PDF pages, decide whether you want one document containing selected pages or a separate file for every page. For a packet with a cover sheet, a form and supporting notes, the extract option lets you send the form on its own. If each page is an invoice for a different customer, individual downloads make it easier to route the files."),
            ("Pull three pages from a seven-page packet", "Suppose the cover is the first page, the form fills pages 2 and 3, and the signature sheet is page 7. Enter 2-3,7 and choose the extract option. The output contains three pages in this order: 2, 3 and 7. If you enter 7,3,2,3 instead, the tool still gives you the same three pages. It removes duplicates and follows the order of the source PDF."),
            ("Use the position in the file", "Count from the first sheet in your PDF viewer. A book might print page 1 after a cover and four pages of front matter. In that file, printed page 1 is the sixth page. If you use the printed label as your input, you may extract the cover instead. Compare the output with the source before attaching it to an email or submitting a form."),
            ("Separate every page when the file is short", "Choose the individual option to create one download link per page. A four-page document produces four links. Each download is its own PDF, so you can open or share it separately. For a longer document where you need only a few sheets, use the extract option and specify those positions. Individual mode accepts up to 50 pages, and the input file limit is 100 MB."),
            ("Check the new file before sending it", "Extraction leaves the source file intact and runs in your browser. Make sure the output contains only the pages the recipient should see, and open it to check the order. If you plan to rearrange pages from more than one document, extract smaller files first and then combine those files with Merge PDF. Password-protected PDFs need to be unlocked before they can be processed here."),
        }, new FaqItem[]
        {
            new("How do I save pages 2 through 5?", "Choose extract mode and enter 2-5."),
            new("How do I save nonconsecutive pages?", "Separate positions with commas, such as 2,5,8."),
            new("Can I save each page as a separate PDF?", "Yes. Choose the individual mode for PDFs of up to 50 pages."),
            new("Can I reorder the extracted pages in the range box?", "The extractor always follows the original page order."),
            new("Will extracting pages change my original file?", "The tool creates a separate copy and leaves the original in place."),
            new("Are PDF uploads required?", "Your PDF is read and split in your browser."),
        }),
        ["compress-pdf"] = (new (string, string)[]
        {
            ("When to compress PDF scans", "Use Compress PDF for scans and photo-heavy documents that are too large to attach or upload. A scanned page can contain a full-page photograph even when it looks like a simple form. The tool redraws each page, turns the drawing into a JPEG and combines the JPEGs in a new PDF. How much space you save depends on the source and the selected quality setting."),
            ("Estimate the reduction you need", "Say a portal accepts files up to 2 MB and your scan is 5 MB. You need to remove at least 3 MB, which is 60% of the original size. Start with Balanced and check the downloaded file size in your file manager. The result line summarizes the change, but its labels are rounded. If the limit is strict, leave some margin and confirm that the file is safely below it."),
            ("Choose the setting by what people must read", "Balanced is a useful first attempt for a typical scan. Try Smallest file if the output still exceeds the limit. Use Best quality when small type, fine lines or handwritten marks are important, and zoom in to inspect those details before sending. A smaller file can be harder to read, so keep your original in case you need a clearer copy later."),
            ("Protect searchable text and interactive fields", "This method makes a flat image of every page. You will be able to see the text, but you cannot select or search it in the new file. If you need searchable text, working links or fillable fields, use a PDF optimizer that preserves those features. If the recipient only needs a few pages, try Split PDF first. Keeping fewer pages may meet the limit without changing how those pages look."),
            ("Know when to stop compressing", "The tool accepts up to 40 pages and 100 MB. The processing happens in your browser, so give it time to finish on a phone. Some PDFs already store their pages in a compact form, and converting them to images can make the result larger. If compression reports no gain, keep your original or try the smaller preset."),
        }, new FaqItem[]
        {
            new("Can I choose an exact target size?", "Choose a quality preset, then check the resulting size against your limit."),
            new("Does this work for scans?", "Scans and photo-heavy PDFs are good candidates for this method."),
            new("Will I still be able to search the text?", "The output pages are images, so their text is no longer searchable."),
            new("Why did I get a no-gain message?", "The compressed copy was the same size or larger than the source."),
            new("How much can I compress at once?", "The tool accepts one PDF of up to 40 pages and 100 MB."),
            new("Does my PDF go to a server?", "The document stays in your browser."),
        }),
        ["gpa-calculator"] = (new (string, string)[]
        {
            ("How this GPA calculator weights courses", "The GPA calculator estimates a grade point average on a 4.0 scale using up to three courses. Give each course its grade points and credits. Grade points measure performance in one course. Credits determine how much that course counts toward the average. A four-credit course has twice the weight of a two-credit course."),
            ("Reproduce the default result", "The default courses have grades of 4.0, 3.0 and 3.7 with three, four and three credits respectively. Multiply each grade by its course credits to get 12, 12 and 11.1 grade points weighted by credits. The total is 35.1. Divide by the ten credits to get 3.51. Averaging the three grades directly gives about 3.57, because that method gives every course the same weight."),
            ("Convert letter grades before entering them", "Use the grading scale in your school's catalog or student portal. A letter grade can have a different numerical value depending on your institution, especially when it has a plus or minus. Enter the number from the scale your school uses. The calculator accepts values from 0 to 4, which covers unweighted 4.0 scales. A school that awards more than 4.0 points for advanced courses needs a different calculation."),
            ("Handle unused rows and failing grades", "Set the credits to zero for any course row you do not need. Enter zero grade points for a failing grade only if zero is the value your school assigns to it, and keep the course credits if they count toward GPA. A zero-credit row contributes nothing to the average. A zero-point grade with positive credits adds credits to the divisor and can lower the result."),
            ("Read the estimate alongside your transcript", "The result is rounded to two decimal places for display. If you need an official GPA for an application, use the value your school supplies. Check its rules for withdrawals, repeated courses and pass or fail grades. If you are only trying to see what score to aim for on your next exam, use the Grade Calculator to model your grade within that course."),
        }, new FaqItem[]
        {
            new("How do I calculate a credit-weighted GPA?", "Multiply each grade point value by credits, add the results and divide by the total credits."),
            new("Can I enter just two courses?", "Set the unused row to zero credits and leave its grade points at a valid value."),
            new("Can I type a letter grade?", "Convert it to grade points using your school's scale first."),
            new("Can I enter an AP grade above 4.0?", "This calculator accepts grade points from 0 to 4."),
            new("Does zero grade points equal zero credits?", "Zero grade points with positive credits still affects your GPA."),
            new("Is the displayed GPA official?", "Use your school transcript or student portal for your official GPA."),
        }),
        ["tip-calculator"] = (new (string, string)[]
        {
            ("Use the tip calculator to split a bill", "The tip calculator works out a percentage tip, adds it to the bill you enter and divides the total equally among the people paying. Enter the bill in dollars, enter the number of people and choose a preset or type your own percentage. The result includes each person's share of the tip so you can separate it from the price of the meal if your payment app asks."),
            ("Work through an 18% tip", "For a $60 bill, divide 18 by 100 to get 0.18 and multiply by $60. The tip is $10.80. Add that to get $70.80, then divide by two for $35.40 per person. Each person's share includes $5.40 in tip. To see how a different percentage changes the total, leave the bill and number of people as they are and choose another preset."),
            ("Check what your receipt includes", "Look for an automatic gratuity or service charge on the receipt before deciding how much extra to leave. If you want to calculate the tip on the pre-tax subtotal and pay the full taxed total, use the subtotal here for the tip. Then add the displayed tip to the charged total on your receipt and divide that amount among the people paying."),
            ("Handle an extra cent in an even split", "The displayed share is rounded to cents. A $10 bill split three ways with a zero percent tip shows $3.33 per person. Three payments of $3.33 add up to $9.99. One person can pay $3.34 to cover the remaining cent. When a payment screen shows a different share by a cent, check how it assigned the remainder."),
            ("Calculate separate shares for different orders", "An even split makes sense when the group agrees to share the cost equally. If one person ordered a $20 meal and another ordered a $40 meal, each can calculate a tip using their own share of the bill and one person. Decide how to allocate shared items, tax and any room charge first, so the individual base amounts still add up to the receipt."),
        }, new FaqItem[]
        {
            new("What is 18% on $60?", "The tip is $10.80 and the total is $70.80."),
            new("Can I enter my own tip rate?", "Yes. Type a percentage in the Tip field."),
            new("Can I split a bill with no tip?", "Enter 0% and the number of people paying."),
            new("Should I enter the amount before tax?", "Enter the amount you want to use as the base for the tip."),
            new("Why can the shares be off by a cent?", "Individual shares are rounded to cents, which can leave a remainder."),
            new("Can each person pay for their own order?", "Calculate each individual bill separately with the people field set to one."),
        }),
        ["discount-calculator"] = (new (string, string)[]
        {
            ("Find a sale price with the discount calculator", "The discount calculator shows the price after one or two percentage discounts and how much you save in dollars. Enter the original price and the first discount. If a store allows an additional percentage coupon on the sale price, enter it in the second box. Use zero for the extra discount if the advertised offer is the only one that applies."),
            ("A $120 item with two discounts", "If a $120 item is 25% off, you pay 75% of the original price, which is $90. A coupon for another 10% off the sale price removes $9 from that $90. The final price is $81, and the savings are $39. Divide $39 by $120 to see that the effective discount is 32.5%. Adding the percentages would give 35%, which overstates the actual savings."),
            ("Check the amount each offer applies to", "A store may advertise a discount on the whole order, on one category, or on the least expensive item. Use the eligible subtotal as your starting price. For a $10 coupon, subtract $10 at the stage specified by the offer and calculate the percentage discounts separately. Putting 10 into a percentage field would instead take one tenth of the applicable price."),
            ("Compare the total you will actually pay", "This estimate uses the item price only. It excludes sales tax, shipping and other checkout charges. If you are comparing two stores, include delivery and any required fees before you decide. If you calculate sales tax separately, check the current figure and ask the seller which amount is taxable. The rules can depend on the location, the item and how the coupon is applied."),
            ("Match the result to a checkout receipt", "The tool applies the discounts in order and displays dollars to the nearest cent. A retailer might round on each line or at each step, so a receipt can differ by a cent. Some promotions exclude already discounted items or prohibit combining offers. Check the actual terms before relying on a stacked estimate, especially if you are deciding whether to buy."),
        }, new FaqItem[]
        {
            new("How much is 25% off $120?", "The sale price is $90 before any extra discount or charges."),
            new("How do I stack two percentage discounts?", "Enter the original price and both percentages. The second discount applies to the first sale price."),
            new("Is 25% off plus 10% off the same as 35% off?", "Applied in sequence, those discounts save 32.5% of the original price."),
            new("Can I enter a $10 coupon?", "The fields accept percentages. Subtract a fixed coupon separately using the offer terms."),
            new("Does the result include sales tax?", "The estimate excludes tax, delivery and other checkout charges."),
            new("Why does my receipt differ by a cent?", "The retailer may round the discount at a different stage or per item."),
        }),
        ["days-until-calculator"] = (new (string, string)[]
        {
            ("Count down with the days until calculator", "The days until calculator compares an event date with today's date on your device. It shows days remaining for a future date and days elapsed for a past date. Use it for trips, birthdays and school breaks when a whole calendar day is the unit you care about. The default event date is one week from today, so the first calculation shows seven days remaining."),
            ("Count the steps between dates", "Imagine today is November 26 and your event is December 3 of the same year. November 27 is one day away, November 30 is four days away and December 3 is seven days away. A countdown shows the gap between the dates. If you are planning meals and need one for every date from November 26 through December 3, count eight dates instead."),
            ("Expect the answer to change tomorrow", "The calculation reads the date shown by your device and compares calendar positions. When midnight passes on your device, calculate again to see the new count. On a trip, your phone may update to a different time zone before the people back home reach midnight. Two people in different time zones can therefore see different counts for the same event date."),
            ("Check which days your plan uses", "The countdown includes weekends and holidays. For an office turnaround quoted in business days, check the office calendar for holidays and closures. If your event starts at a particular hour, verify the local time separately. Time zones and clock changes matter when you need hours remaining. Here, the difference is measured in dates, so a transition to daylight saving time still counts as one calendar day."),
            ("Compare two other dates when needed", "If you want the gap between two dates that are both in the future, use a date difference calculator. Use this countdown when one end of the calculation should always be today. For an application deadline, follow the organizer's stated cutoff and time zone. A countdown gives you the number of days to plan around, and the organizer's instructions determine when submissions close."),
        }, new FaqItem[]
        {
            new("How many days until a date one week away?", "Seven calendar days."),
            new("Does the calculator include today?", "Today is the starting point, so an event today has a gap of zero."),
            new("Can I choose a past date?", "Yes. The result shows how many days have passed since that date."),
            new("Are weekends included?", "Yes. The calculator counts all calendar days."),
            new("Why did my result change during travel?", "Your device may have switched to a different local date."),
            new("Does it count hours?", "The result uses whole calendar days."),
        }),
        ["car-loan-calculator"] = (new (string, string)[]
        {
            ("What the car loan calculator estimates", "The car loan calculator estimates the fixed monthly payment on a vehicle purchase. Enter the vehicle price, subtract your cash down payment and net trade-in credit, then enter the annual interest rate and term in months. The difference between the purchase price and the two credits is the principal you are borrowing. Check the current figure with a lender when you enter a rate."),
            ("Work through a $30,000 car example", "For a $30,000 vehicle with $3,000 down and no trade-in, the principal is $27,000. At an illustrative 7% annual interest rate over 60 months, the estimated monthly payment is $534.63. Using the unrounded payment, total payments are about $32,077.94, including roughly $5,077.94 in interest. The down payment is separate from the loan repayment total."),
            ("See how the monthly formula works", "Divide the annual interest rate by 100 and then by 12 to get the monthly rate r. Multiply the principal P by r, then divide by one minus (1 + r) raised to the power of negative n, where n is the number of monthly payments. At zero interest, divide principal by n. The model assumes equal monthly payments throughout the term."),
            ("Use the trade-in credit you actually keep", "If the dealer offers $8,000 for your trade-in and the payoff on that car is $5,000, the net credit is $3,000. Enter that amount rather than the full offer. If you owe more than the trade-in is worth and the shortfall is financed, ask for the amount rolled into the new loan and include it in the financed costs when you compare quotes."),
            ("Compare the written offers", "Sales tax, title fees and financed extras are excluded unless you include them in the price you enter. Insurance, fuel and repairs are separate ownership costs. The contract may also calculate interest by day, so request the written loan offer and compare amount financed, term, APR and total payments before signing. Ask your lender to explain any fees or charges you cannot reconcile."),
        }, new FaqItem[]
        {
            new("What is the payment on $27,000 at 7% for 60 months?", "About $534.63 per month under the calculator's assumptions."),
            new("Should I use APR or the interest rate?", "Use the annual interest rate in the payment formula. Compare APR separately when reviewing lender offers."),
            new("Do I enter the full trade-in value?", "Enter the credit left after paying off any balance on the trade-in."),
            new("Can I enter zero interest?", "Yes. The tool divides the financed amount by the number of months."),
            new("Does the estimate include title or sales tax?", "Include any financed tax and fees in the purchase price yourself."),
            new("Does the total repaid include my down payment?", "It shows payments on the financed balance. The down payment is separate."),
        }),
        ["mortgage-calculator"] = (new (string, string)[]
        {
            ("What this mortgage calculator includes", "The mortgage calculator combines a fixed-rate loan payment with monthly estimates for property tax and homeowners insurance. Subtract your down payment from the purchase price to find the principal. Supply an annual interest rate and a term in years. Ask the lender for an actual quote and check the current figure for the property's tax and for insurance before budgeting."),
            ("Separate the loan payment from housing costs", "A $400,000 house with $80,000 down leaves a $320,000 loan. At an illustrative 6.5% annual rate over 30 years, principal and interest are about $2,022.62 a month. Estimated property tax of $4,000 and insurance of $1,500 add $458.33 a month. Together they give about $2,480.95 a month before other costs."),
            ("Recreate the fixed-rate formula", "Convert years to months by multiplying by 12. Convert the annual percentage to a monthly decimal rate by dividing it by 1,200. Multiply the principal by the monthly decimal rate, and divide by one minus (1 + monthly rate) raised to the negative number of months. For a zero interest loan, divide principal by months. The model assumes regular monthly payments and a constant rate."),
            ("Know which figures can change", "Principal and interest for a typical fixed-rate loan stay level with scheduled payments, while taxes and insurance can rise or fall. If your lender collects these amounts in escrow, check the projected payment in the written Loan Estimate. If you pay the bills directly, the monthly figures here are useful for deciding how much to set aside. They may differ from the amount you send the lender each month."),
            ("Review costs outside the calculation", "Mortgage insurance, association dues, closing costs and repairs are outside this model. The total repaid and interest figures apply to the loan itself. Use an official Loan Estimate to compare lenders and ask your loan officer which amounts are included in the projected payment. If you are choosing between loans with different rates and fees, review both the monthly payment and the cash needed at closing."),
        }, new FaqItem[]
        {
            new("What is the estimated payment on a $400,000 home with $80,000 down?", "At 6.5% for 30 years, principal and interest are about $2,022.62 a month."),
            new("Do I enter property tax monthly?", "Enter the annual property tax. The tool divides it by 12."),
            new("Is mortgage insurance included?", "Add any mortgage insurance separately using your Loan Estimate."),
            new("Is annual interest rate the same as APR?", "APR can reflect certain borrowing costs. Enter the annual interest rate for this payment formula."),
            new("Will the estimated total match my escrow payment?", "Use your lender's projected payment to confirm what the escrow portion includes."),
            new("Can this estimate an adjustable-rate mortgage?", "It models a fixed rate across the entire term."),
        }),
        ["hourly-to-salary-calculator"] = (new (string, string)[]
        {
            ("Go from hourly pay to yearly gross pay", "The hourly to salary calculator estimates gross annual pay by multiplying your hourly rate by paid hours per week and then by paid weeks per year. Gross pay is the amount before withholding and deductions. Enter the hours you expect to be paid for, including paid leave when it is paid at your usual rate."),
            ("Work the numbers at $25 an hour", "At $25 per hour and 40 paid hours each week for 52 weeks, the annual estimate is $52,000. A fully paid week is $1,000. Spread the annual estimate over 12 months and it averages $4,333.33 per month. Spread it over 26 equal payments and it averages $2,000 every two weeks. A monthly average may differ from what appears on an individual paycheck."),
            ("Adjust for time off without pay", "Consider a schedule with 50 paid weeks instead of 52. At $25 an hour for 40 hours a week, the annual result falls to $50,000. One fully worked week is still worth $1,000. The average across the calendar year is lower because two weeks generate no pay. Reduce the weeks figure for an unpaid break if your remaining paid weeks follow the same schedule."),
            ("Convert a salary offer to an hourly equivalent", "To go the other direction, divide annual salary by paid hours in a year. For $52,000 and 2,080 paid hours, the hourly equivalent is $25. Use the hours and weeks above when you change the annual salary. If two salaried roles pay the same amount but expect different weekly hours, run the calculation for each schedule to see how the hourly equivalents differ."),
            ("Use a pay stub to check actual take-home pay", "The model assumes a flat rate and a stable schedule. Overtime premiums, tips, bonuses and shift differentials are outside the calculation. It also excludes withholding, retirement contributions and insurance deductions. If deciding whether a new offer covers your bills, ask the employer about their pay schedule and deductions. Consult payroll or a qualified tax professional for questions about your withholding."),
        }, new FaqItem[]
        {
            new("How much is $25 an hour per year?", "$52,000 gross at 40 hours per week for 52 weeks."),
            new("Can I use a part-time schedule?", "Yes. Enter your usual paid hours per week and paid weeks per year."),
            new("Does monthly pay equal four weekly checks?", "This tool divides annual pay by 12 to calculate a monthly average."),
            new("Does the estimate include overtime?", "It applies one hourly rate. Calculate overtime premiums separately."),
            new("Do I count unpaid vacation?", "Reduce paid weeks or paid hours to account for unpaid leave."),
            new("Can I convert a yearly salary to hourly pay?", "Enter the annual salary and use the hourly-rate button with the same hours and weeks."),
        }),
        ["sales-tax-calculator"] = (new (string, string)[]
        {
            ("Use the sales tax calculator in either direction", "The sales tax calculator can add a percentage to a pretax price or work backward from a tax-inclusive total. Enter the rate yourself and use the section that matches the numbers you have. Check the current figure with the relevant tax authority before applying it. The sample rate is for testing the calculation and does not identify a location."),
            ("Add tax to a $100 purchase", "For a $100 purchase and an example rate of 7.25%, divide 7.25 by 100 to get 0.0725. Multiply the price by that decimal to get $7.25 of tax. Add it to the price for a $107.25 total. The result assumes the entire price is taxable at the entered rate. Use a smaller taxable subtotal if part of the purchase is exempt."),
            ("Recover the pretax price from a total", "To reverse the calculation, divide the tax-inclusive amount by one plus the rate as a decimal. For the same example, $107.25 divided by 1.0725 returns $100 before tax. Subtract the original price from the total to find the $7.25 included in the receipt. Multiplying the total by the rate would apply it to a price that already contains tax."),
            ("Check the combined rate and taxable subtotal", "A purchase can have a combined rate made up of more than one local or state component. Use the applicable combined figure for the purchase, and check whether the retailer applied tax before or after any discount. If a receipt contains items taxed differently, calculate those groups separately. Confirm how shipping or fees are treated before entering them in the taxable price."),
            ("Compare the estimate with the receipt", "This tool shows estimated amounts rounded to cents. A retailer may round each line before summing the receipt, which can leave a small difference from tax calculated on one subtotal. The tool cannot decide which rate or exemption applies. For tax collection, reporting or a disputed receipt, check the current rule with the relevant authority or a qualified tax professional."),
        }, new FaqItem[]
        {
            new("What is 7.25% sales tax on $100?", "The tax is $7.25 and the total is $107.25."),
            new("Can I remove tax from an amount already paid?", "Divide the tax-inclusive total by one plus the rate expressed as a decimal."),
            new("Does the tool find a rate by ZIP code?", "Enter the rate that applies to the purchase."),
            new("Can I enter a combined state and local rate?", "Yes. Enter the combined percentage that applies to the taxable price."),
            new("Why is tax on the receipt off by a cent?", "The retailer may calculate and round tax per line."),
            new("Can I use the same rate for every item?", "Check whether any items are exempt or taxed differently."),
        }),
        ["pdf-to-jpg"] = (new (string, string)[]
        {
            ("When to convert PDF to JPG", "Convert PDF to JPG when a form accepts images or when you need a picture of a page for a slide. The converter renders each whole page, including the text and graphics you can see, as its own JPG. Choose the document, leave the resolution at Standard for a first pass and select Convert to JPG. Download the images you want from the links below the result."),
            ("Pick a resolution by pixel width", "A US Letter PDF page is 612 by 792 PDF points. At Standard, the tool scales each dimension by 1.5 and produces a 918 by 1,188 pixel image. At High, it scales by 2.5 for 1,530 by 1,980 pixels. High gives you more pixels to work with when fine text matters. Check any exact pixel requirements from the destination before choosing."),
            ("Keep a searchable copy of the document", "A JPG captures how a page looks. Its text becomes pixels, so you cannot select a sentence, follow a link or fill an interactive field in the picture. Keep the original PDF if someone needs those features. If an upload asks for a JPEG preview and a full document, send both files separately so the recipient has the right version for each task."),
            ("Export only the pages you need", "Each image is named by its position in the input document, starting with page 1. If the page number printed in the footer differs from that position, check each image before sending it. This tool converts at most the first 30 pages per run. Use Split PDF first to extract later pages, then convert the smaller PDF to JPG."),
            ("Check size and readability", "The converter accepts one PDF up to 100 MB. Very large pages may require Standard or a smaller PDF. JPG uses lossy compression, so inspect thin lines and small text at the zoom your recipient will use. Processing happens in your browser and leaves the original PDF untouched. If you need to stay below a file size limit, compress the exported image and recheck it."),
        }, new FaqItem[]
        {
            new("Can I convert a multipage PDF?", "Yes. The first 30 pages become separate JPG downloads."),
            new("How many pixels is a Letter page at Standard?", "A 612 by 792 point page renders at 918 by 1,188 pixels."),
            new("Can I copy text from the JPG?", "Keep the PDF for searchable or selectable text."),
            new("Can I save only page 40?", "Extract page 40 using Split PDF and convert the extracted file."),
            new("Will the converter upload the PDF?", "Your PDF is processed in your browser."),
            new("Can it open a locked document?", "Open or unlock the PDF with the correct password first."),
        }),
        ["png-to-jpg"] = (new (string, string)[]
        {
            ("Use PNG to JPG for JPEG-only uploads", "Use PNG to JPG when a form or app asks for a JPEG file and your original is a PNG. Choose the source images and convert them in the browser. Each output has the same pixel dimensions as its source. The tool fills transparent areas with white before saving the picture as JPEG, so check how light edges and lettering look on that background."),
            ("What happens to transparent margins", "Imagine a 1,200 by 800 pixel PNG with a transparent border. The output is still 1,200 by 800 pixels. Every fully transparent border pixel is drawn over white. Partly transparent edges blend into that background, which helps smooth the transition around shapes. JPEG compression can introduce tiny changes near edges, so look closely if you plan to put the image on a dark surface."),
            ("Pick the version that fits the image", "JPEG often works well for photos and other pictures with many smooth color changes. Keep the PNG when a background needs to stay transparent or a graphic depends on sharp one-pixel lines. Inspect screenshots at their normal reading size. A JPEG can show soft edges around thin text, especially if the source has crisp black letters on a plain white screen."),
            ("Separate format conversion from resizing", "The converter changes the file format and leaves width and height alone. If a form asks for a 600 pixel wide image, use Resize Image to set that width. If it asks for a size in KB, use Compress Image to target the size separately. Check both requirements when the form lists them together. A correct format can still exceed a pixel or file size limit."),
            ("Convert copies in manageable groups", "You can choose up to 20 files per run, each at most 20 MB. Very large pixel dimensions can exceed the browser's image limit. The result lists each successful download and identifies files that failed. Compare file sizes after conversion, because a JPEG can be larger than a simple PNG. Keep the original PNG if you will need to edit it or use its transparency again."),
        }, new FaqItem[]
        {
            new("Does the JPG keep the same dimensions?", "Yes. Conversion preserves the source width and height in pixels."),
            new("What happens to a transparent background?", "The converter draws transparent areas over white."),
            new("Will the JPG always be smaller?", "File size depends on the picture. Compare both files after converting."),
            new("Can I convert a folder of images?", "Select up to 20 compatible files in one run."),
            new("Can I enter a maximum size such as 100 KB?", "Use Compress Image to target a file size after conversion."),
            new("Does my PNG leave my device?", "The conversion runs in your browser."),
        }),
        ["jpg-to-png"] = (new (string, string)[]
        {
            ("When to use JPG to PNG", "Use JPG to PNG when a design tool, online form or editing step specifically needs a PNG. Choose one or more JPEG photos and download the converted files. The browser reads the visible picture and saves those pixels as PNG without resizing. PNG encoding is lossless, so that save step avoids another round of JPEG quality loss on the decoded picture."),
            ("Follow the dimensions through the conversion", "Start with a JPEG that is 1,200 pixels wide and 800 pixels high. The decoded picture holds 960,000 pixels. Conversion produces a PNG with the same 1,200 by 800 visible dimensions. Saving to a new format cannot reconstruct details discarded when the picture was originally compressed. Existing blocks around text or blur from the camera will still be visible."),
            ("Keep transparency expectations realistic", "A PNG can store transparent pixels, but a JPEG contains an opaque picture. For example, a JPEG with a white wall behind a product still has that wall after conversion. To get a transparent background you need to edit the picture and remove the background first. Then choose PNG when you save the edited result so the transparent areas remain available."),
            ("Check file size before you attach the result", "A photographic PNG often takes more space than the JPEG it came from. If you convert an image just to upload it, compare the PNG size with the site's limit. Where JPEG is also accepted, the original file can be the smaller choice. If the PNG needs fewer pixels, resize it first and check whether the resized file still suits its intended use."),
            ("Preserve the original and its information", "This tool handles up to 20 files at a time, with a 20 MB limit per file and a separate pixel limit to protect browser memory. Output names use the source name with a PNG extension. Browser export makes a fresh image, so keep the original JPEG if the camera's embedded information matters. Check the PNG visually before replacing anything in your workflow."),
        }, new FaqItem[]
        {
            new("Will the PNG look sharper?", "The output keeps the visible JPEG picture. It cannot restore details discarded earlier."),
            new("Does the converter make the background transparent?", "Background pixels stay visible unless you remove them in an editor."),
            new("Are pixel dimensions preserved?", "Yes, the converted picture uses the decoded width and height."),
            new("Will the PNG use less storage?", "Photographic PNGs are often larger. Compare the actual files."),
            new("Can I convert a .jpeg file?", "Yes. Both .jpg and .jpeg are filename extensions for JPEG images."),
            new("How many files can I convert?", "You can select up to 20 files per run."),
        }),
        ["webp-to-jpg"] = (new (string, string)[]
        {
            ("Convert downloaded images with WebP to JPG", "Use WebP to JPG when you saved an image from a website and need a JPEG for a document, editor or upload form. The converter decodes the WebP picture, draws it at its displayed dimensions and encodes the result as a JPEG. Changing the filename extension by hand leaves the original WebP data in place. This tool changes the image data as well as the extension."),
            ("Check the output dimensions", "A still WebP that is 1,280 pixels wide and 720 pixels high has 921,600 pixels. The downloaded JPEG keeps the same dimensions. If the WebP has transparent areas, the JPEG displays them on white. Test the new file against the page or slide where you plan to place it. A white area can stand out if the document itself has a colored background."),
            ("Treat an animated WebP as a different task", "WebP can hold a moving image as well as a still one. A JPEG can store only one picture. This converter captures a still image from what the browser decodes. For a specific moment in an animation, use a frame extraction tool so you can choose the frame yourself. Keep the WebP copy if you may need the animation later."),
            ("Expect the new file size to vary", "Some WebP files are already compact. Converting them to JPEG for compatibility can increase size even though the visible dimensions stay the same. If an upload sets a size cap, check the new file before submitting it. Use Compress Image if you need to reduce the JPEG size. Repeated lossy conversion can soften fine detail, so start from the original when trying again."),
            ("Check rights and keep the source", "Conversion happens in your browser, with up to 20 files per run and a 20 MB limit per file. A new extension does not change whether you may publish an image from someone else's website. Use images you have permission to share. Keep your original beside the converted copy so you have the source for future edits and can compare the two versions."),
        }, new FaqItem[]
        {
            new("Can I just rename .webp to .jpg?", "Renaming leaves the WebP format unchanged. Convert the file to produce a valid JPEG."),
            new("Does the JPG keep the same width and height?", "Yes. The tool uses the decoded dimensions."),
            new("What happens to transparent pixels?", "Transparent areas are drawn over white."),
            new("Does an animated WebP stay animated?", "A JPEG contains one still image."),
            new("Is a JPG always smaller than WebP?", "Size varies. Check the output against the original."),
            new("Does converting require uploading my image?", "The converter processes the image on your device."),
        }),
        ["heic-to-jpg"] = (new (string, string)[]
        {
            ("Use HEIC to JPG for a JPEG-only form", "Use HEIC to JPG when a photo saved in HEIC will not open in your editor or is rejected by a JPEG-only form. Export the original photo to a location you can browse, then choose the file and convert it. The converter uses your browser to decode the photo and creates a separate JPEG that you can download and check before submitting."),
            ("Check dimensions and size after export", "Suppose the downloaded JPEG is 4,000 pixels wide and 3,000 pixels high. It contains 12 million pixels. A portal with a 2,000 pixel width limit would still reject it even though the file is now a JPEG. Use Resize Image to reduce the width to 2,000 pixels and preserve proportions. The matching height is 1,500 pixels. Check the portal's separate file size requirement as well."),
            ("Keep the original for details and metadata", "The output is a newly encoded still image. Keep the HEIC file if you need the original photo quality, camera information or auxiliary images. Open the downloaded JPEG and check that its orientation and colors look as expected. Photos taken in a mode with extra brightness or color information can look different once exported to a conventional JPEG that a form accepts."),
            ("Choose the still photo you intend to share", "A HEIC container can hold multiple images. Each input here produces one JPEG, so check that the downloaded image shows the scene you expected. If you are working with a moving photo, pick the still you want in the originating photo app and export that version before trying to convert it. This tool does not export motion or audio."),
            ("Try smaller groups on a phone", "Choose up to 10 files per run, each up to 20 MB. The decoding work happens locally and can take time on a phone. If a photo fails, try exporting a JPEG copy from the original photo app or try one image at a time. Do not delete your original until you have checked the new file and confirmed that the recipient can open it."),
        }, new FaqItem[]
        {
            new("Why does my form reject HEIC?", "It may require a different format such as JPEG. Check the accepted types."),
            new("Can I convert several photos?", "Yes. Select up to 10 compatible files per run."),
            new("Will I get the video from a moving photo?", "The output is a still JPG."),
            new("Does it preserve all camera information?", "Keep the source file when metadata is important."),
            new("Is the JPEG guaranteed to be smaller?", "No. Check the downloaded size against the limit you need to meet."),
            new("Does this tool upload my phone photos?", "The files stay in your browser."),
        }),
        ["case-converter"] = (new (string, string)[]
        {
            ("Choose a mode in the case converter", "The case converter changes capitalization for headings, sentences and short labels. Paste your text and choose a mode. Uppercase and lowercase apply to the whole selection. Title Case capitalizes letters at the start and after selected boundaries such as spaces and hyphens. Sentence case starts the first sentence with a capital and looks for punctuation followed by a space to find the next one."),
            ("Make a label fit a naming convention", "Start with North Shore Tickets 2026. The camelCase result is northShoreTickets2026. The snake_case version is north_shore_tickets_2026, and the kebab-case version is north-shore-tickets-2026. Those modes split the label on punctuation or spaces and join the resulting words with the chosen separator. Use the original phrase with spaces if you need to try several naming styles from the same source."),
            ("Read headlines before publishing them", "This Title Case mode uses mechanical capitalization. Some editorial styles keep short articles and conjunctions lowercase. Adjust those words by hand when your style guide calls for it. Sentence case can also change proper names and abbreviations, because it lowercases the text before looking for sentence starts. Compare the result with the original if it contains company names, acronyms or words with unusual capitalization."),
            ("Keep punctuation only where you want it", "Uppercase and lowercase leave punctuation and spacing in place. The identifier styles use letters and numbers, including letters from many alphabets, and discard separators outside the words. If you are creating a name for a specific programming language, check its identifier rules. Adding an initial digit or using a reserved word can make an otherwise readable converted name unusable in code."),
            ("Work from a copy of your original", "Each button replaces the text in the editable box. If you change it to lowercase, the page cannot infer which words were previously capitalized. Keep a copy of the original when you need to preserve its style. To try the tool, start with the sample text. Your conversion runs in the browser and you can copy the edited value for use elsewhere."),
        }, new FaqItem[]
        {
            new("Can I change all the letters to uppercase?", "Press UPPERCASE to transform the text in the box."),
            new("Does Title Case follow AP style?", "It capitalizes mechanically. Apply any editorial style rules by hand."),
            new("What is snake_case?", "It joins lowercase words with underscores."),
            new("Does camelCase retain punctuation?", "It joins word tokens and drops punctuation between them."),
            new("Will sentence case preserve acronyms?", "It can lowercase acronyms. Restore the intended capitals after conversion."),
            new("Can I start again?", "Paste your original again or reload the page to restore the example."),
        }),
        ["json-formatter"] = (new (string, string)[]
        {
            ("Use the JSON formatter to see structure", "The JSON formatter validates syntax and can display nested objects and arrays with two spaces of indentation. Paste a document and choose Format for a readable view, Minify for a compact representation, or Validate to check syntax while leaving your text in place. Blank lines and spaces outside strings can change how JSON looks without changing the text inside its quoted values."),
            ("Work through a small configuration", "Paste a compact object with two fields, for example {\"items\":[1,2],\"label\":\"New York\"}. Format gives the object keys their own lines and puts the two array elements on separate lines. Minify removes the indentation and line breaks. In both results the array still contains two elements, and the space between New and York remains because it sits inside a quoted string."),
            ("Understand what Validate checks", "JSON requires double quotes around object keys and string values. A trailing comma after the final member of an object or array will fail validation. JavaScript object literals can contain syntax that JSON does not, so a snippet copied from source code may need editing. Validation checks the syntax of the text and does not check whether an app expects particular keys or types."),
            ("Preserve how numbers and strings were written", "The tool checks syntax and changes whitespace around tokens. It leaves the original number spelling, string escapes and key order in place. For example, the integer 9007199254740993 stays as entered. Some JavaScript applications will still round it when they read it into a standard number. Repeated keys can also be interpreted differently across software, so check the consuming app if your document uses them."),
            ("Know when to use a file-based formatter", "The input limit is 200,000 characters, and formatting deeply nested documents may hit an additional limit. Very large or deeply generated files are better handled in a local editor that can process them as files. Copy the result only after choosing the mode you want. Formatting is done in your browser, and the page leaves invalid input available for you to correct."),
        }, new FaqItem[]
        {
            new("What does pretty printing do?", "It puts values on separate lines and indents nested levels by two spaces."),
            new("Can minifying remove a space inside a string?", "Spaces inside strings are preserved."),
            new("Does validation check against a JSON Schema?", "It checks JSON syntax. Use a schema validator for required fields and types."),
            new("What happens to large integers?", "Their written digits stay intact during formatting. The receiving application may interpret them differently."),
            new("Are trailing commas valid?", "JSON requires the last item or member to end without a trailing comma."),
            new("Is there a size limit?", "Input is limited to 200,000 characters. Large formatted documents can hit an additional limit."),
        }),
        ["base64-encoder"] = (new (string, string)[]
        {
            ("Use the Base64 encoder for text fields", "The Base64 encoder turns UTF-8 text into characters that can pass through systems that expect Base64. Paste text and select Encode to get the encoded representation. Paste standard Base64 that contains UTF-8 text and select Decode to get the message back. The tool works on the text you enter and leaves it in the editable box so you can use the reverse operation."),
            ("See what happens to one letter", "The letter A is written as one UTF-8 byte, with decimal value 65. Encoding that byte gives QQ== in standard Base64. The equals signs are padding. Decode QQ== and you get A again. A non-ASCII character can use multiple bytes in UTF-8. For example, the single character é takes two bytes and encodes as w6k=. The number of characters alone does not determine the encoded length."),
            ("Check the variant expected by the destination", "Standard Base64 uses plus and slash among its symbols, and it can end with equals signs. A field that specifically asks for Base64url may expect different characters. This tool handles the standard alphabet. If you copy from a source that wraps Base64 into lines, the decoder ignores whitespace. Check that you copied the entire encoded value, including its ending, before decoding."),
            ("Recognize text versus arbitrary bytes", "Base64 can represent any bytes, including image data and other binary files. The Decode button here expects bytes that form valid UTF-8 text. An encoded image can therefore fail even when its Base64 is syntactically valid. Use a file decoder when you need a downloadable binary result. If the source text uses another character encoding, convert it to UTF-8 with an appropriate text editor first."),
            ("Understand who can read the result", "Anyone who has the encoded text can decode it. Base64 changes representation and provides no secrecy. Use an approved security mechanism for private credentials or messages. The encoding runs in your browser. Use sample text when you are learning the format, and check the recipient's requirements before deciding whether a text field needs Base64 or a different encoding."),
        }, new FaqItem[]
        {
            new("What is Base64 for A?", "The standard Base64 result is QQ==."),
            new("Can I encode accented letters?", "Yes. The encoder converts the text to UTF-8 before encoding."),
            new("Can I decode Base64 with line breaks?", "The decoder ignores whitespace."),
            new("Does it decode arbitrary files?", "It returns valid UTF-8 text. Use a file decoder for binary data."),
            new("Is Base64 encrypted?", "Anyone with the Base64 string can reverse the encoding."),
            new("Does it use Base64url?", "The tool uses the standard Base64 alphabet."),
        }),
        ["url-encoder-decoder"] = (new (string, string)[]
        {
            ("Use the URL encoder decoder for one component", "The URL encoder decoder transforms a single value such as the text for a search parameter. A URL uses characters such as ampersand and equals sign to separate its parts. When those characters belong inside your value, encode the value before inserting it. The decoder reverses percent escapes, allowing you to read a value that was copied out of a URL."),
            ("Encode a search for cats and dogs", "The example cats & dogs becomes cats%20%26%20dogs. Each space becomes %20 and the ampersand becomes %26. You can put the result after q= in a search URL, leaving the question mark and the equals sign as separators. If you paste and encode the entire URL at once, the punctuation that makes the URL navigable can also be encoded."),
            ("Know when a plus sign stays a plus", "This tool represents a space as %20. Some forms use a plus sign for a space under a different convention. The decoder here leaves a literal plus sign unchanged and turns %2B into a plus. If you are reading data created by a form that uses plus for spaces, decode it with that form convention. Check the expected format before transforming addresses or sharing links."),
            ("Watch for a value that is already encoded", "A percent sign becomes %25 when you encode it as literal text. For example, encoding the already encoded sequence %20 gives %2520. Decoding that once returns %20, and decoding again returns a space. Treat encoding as a step you apply once per layer. If a site shows percent codes in its search box, verify that the value was encoded only as many times as the site expects."),
            ("Handle non-ASCII text and invalid sequences", "The tool uses UTF-8 when it turns characters such as é into percent escapes. The encoded form of é is %C3%A9. A sequence like %ZZ cannot represent a hexadecimal byte. When decoding fails, check for a missing character after a percent sign or for bytes that fail to form UTF-8. The operation runs locally, and you can fix the input without leaving the page."),
        }, new FaqItem[]
        {
            new("How do I encode an ampersand?", "An ampersand inside a value becomes %26."),
            new("How is a space encoded?", "This tool uses %20 for a space."),
            new("Does decoding turn plus into space?", "A literal plus sign stays a plus in this component decoder."),
            new("Should I encode an entire URL?", "Encode the individual value, then place it into the URL with its separators."),
            new("Why did %20 turn into %2520?", "Encoding an already encoded value encodes the percent sign too."),
            new("Can I encode accented letters?", "Yes. The encoder uses UTF-8 percent escapes."),
        }),
        ["reverse-text"] = (new (string, string)[]
        {
            ("Choose how to reverse text", "Reverse Text flips either the visible characters or the order of words. Use Characters for a backward string in a puzzle, a short visual experiment or a text processing example. Use Words when you want to keep each word readable and simply move the last one to the front. The result appears below the inputs while your original remains in the text box."),
            ("Compare both results with the default phrase", "For Hello world!, character reversal gives !dlrow olleH and word reversal gives world! Hello. The input has 12 characters if you count the space and exclamation mark. The character mode moves both punctuation and spaces with the rest of the text. The word mode keeps the exclamation mark attached to world because there is no separating whitespace between them."),
            ("Keep characters with multiple parts together", "A visible character can be made from several pieces, such as a letter and a combining accent or an emoji with a skin tone. Character mode uses your browser's text segmentation to reverse groups of visible characters. For example, A👍🏽B becomes B👍🏽A. The skin-tone modifier stays with the thumbs-up emoji. Some writing systems can still display reordered letters differently because their usual joining context has changed."),
            ("Check what happens to spaces and lines", "Character mode reverses the order of the whole input, including spaces and line breaks. Word mode splits the text at whitespace and joins the result using one space between words. Indentation, blank lines and consecutive spaces therefore collapse in word mode. Copy the result into your destination and check it when the original used separate lines or alignment for a particular reason."),
            ("Know where reversal stops", "Reversed text is still ordinary text, so anyone can reverse it again. For character mode, applying the operation twice will recover many simple strings, although writing systems and combining sequences can change how text groups are read. The tool is meant for short passages and runs in your browser. Keep the original when punctuation placement or exact formatting matters."),
        }, new FaqItem[]
        {
            new("Can I reverse words without reversing letters?", "Choose Words to reverse groups separated by whitespace."),
            new("Do punctuation marks move in character mode?", "Yes. Punctuation moves along with the other visible characters."),
            new("How does the tool handle emoji?", "Character mode uses browser segmentation so compound emoji can remain together."),
            new("Does word reversal keep extra spaces?", "It joins words with one space."),
            new("Can I reverse more than one line?", "Yes. Character mode also reverses the order of line breaks."),
            new("Does the result change my input?", "Your original stays in the text box."),
        }),
        ["cm-to-feet-inches"] = (new (string, string)[]
        {
            ("How to convert CM to feet and inches", "Convert cm to feet and inches when a form asks for your height in two boxes. Divide the centimeters by 2.54 to find the total inches. Each full set of 12 inches counts as one foot. The inches left after removing those full feet go into the second box. This tool also displays decimal feet for a form or spreadsheet that expects one number."),
            ("Work through a height of 170 cm", "Divide 170 by 2.54 to get about 66.9291 inches. Five feet account for 60 of those inches, leaving about 6.9291 inches. The converter displays 5 ft 6.93 in and about 5.5774 decimal feet. If the form accepts only whole inches, read its instructions. Rounding the leftover inches to a whole number gives 5 ft 7 in in this example."),
            ("Distinguish feet and inches from decimal feet", "A decimal point in a foot measurement marks a fraction of one foot. For example, 5.5 feet is 5 ft 6 in because half of 12 inches is 6. The same height is 167.64 cm. When a box is labeled feet and offers a separate inches box, put 5 in the first field and 6 in the second. Enter 5.5 only when the field asks for decimal feet."),
            ("Handle a result close to the next foot", "The converter rounds the total inches to two decimal places before splitting the display into feet and inches. If the result rounds to a full foot, the display carries the 12 inches into the feet value. Measure from the original centimeters if you need a different precision. Rounding first to whole centimeters and then rounding inches can shift a result near a boundary."),
            ("Choose the right level of accuracy", "The conversion can only reflect what you entered. A height reported to the nearest centimeter has limited precision even if the calculator shows two decimal places in inches. Use the original measurement when a form accepts centimeters. Use the separate feet and inches result when a fitting chart or height field calls for customary units."),
        }, new FaqItem[]
        {
            new("What is 170 cm in feet and inches?", "About 5 ft 6.93 in, or 5 ft 7 in to the nearest whole inch."),
            new("How many inches are in a foot?", "One foot contains exactly 12 inches."),
            new("Is 5.5 feet the same as 5 ft 5 in?", "5.5 feet means 5 ft 6 in."),
            new("What if rounding gives exactly 12 inches?", "The tool carries the 12 inches into the feet value."),
            new("Can I enter a decimal number of centimeters?", "Yes. Enter a nonnegative measurement such as 170.5."),
            new("How do I get the total in inches?", "Divide centimeters by 2.54 or use the reverse direction in Inches to CM."),
        }),
        ["inches-to-cm"] = (new (string, string)[]
        {
            ("Convert inches to cm in either direction", "Use Inches to CM to compare a measurement on a US product listing with one on a metric tape measure. An inch is exactly 2.54 centimeters. Multiply inches by 2.54 to get centimeters, or divide centimeters by 2.54 to get inches. Edit either field and the other one updates. This tool measures length, so enter a single dimension at a time."),
            ("Check the numbers on a shelf", "A shelf listed as 24 inches wide is 24 × 2.54, or 60.96 cm wide. A space that is exactly 60 cm wide measures about 23.622 inches. Even though both dimensions are close to 60 cm, the shelf would be about 0.96 cm wider than the opening. Confirm the actual measurements and leave clearance for brackets or installation."),
            ("Convert a fraction before entering it", "The input accepts decimals. For 7 1/2 inches, enter 7.5. The conversion gives 19.05 cm. If the product description separately lists width, height and depth, convert each one and keep its label. Writing down the three dimensions in the original order makes it easier to avoid comparing the product height with the space available for its width."),
            ("Use the right units for area", "Centimeters and inches are units of length. To compare two areas, convert both length and width before multiplying them. A 2 inch by 3 inch label measures 5.08 cm by 7.62 cm, so its area is 38.7096 square centimeters. Multiplying the area in square inches by 2.54 would use the length factor only once and give the wrong result."),
            ("Round after checking the fit", "This calculator shows up to four decimal places for typical results and uses scientific notation for unusually small or large values. For rough planning, a whole centimeter may be enough. When cutting a part or checking tight clearance, keep the original measurement and use the precision recommended for the job. A rounded display cannot make a measurement more accurate than the ruler used to take it."),
        }, new FaqItem[]
        {
            new("How many centimeters is 24 inches?", "24 inches is exactly 60.96 cm."),
            new("How do I convert cm back to inches?", "Divide by 2.54 or edit the centimeter field."),
            new("Can I enter a half inch?", "Enter 0.5 to get 1.27 cm."),
            new("Can I use this for square inches?", "Convert each linear side separately before multiplying to find area."),
            new("Is the conversion factor exact?", "Yes. One inch is exactly 2.54 centimeters."),
            new("How do I enter a height in feet and inches?", "Convert feet to inches by multiplying by 12, then add the leftover inches."),
        }),
        ["pounds-to-kg"] = (new (string, string)[]
        {
            ("Convert pounds to kg with the exact factor", "Use Pounds to KG when a scale, parcel label or gym machine shows weight in the other unit. One ordinary pound equals exactly 0.45359237 kilograms. Multiply pounds by that factor to get kilograms and divide kilograms by it to get pounds. The converter works in both directions, so you can type the reading you have without deciding which operation to do."),
            ("Check a weight of 150 pounds", "Multiply 150 by 0.45359237 to get 68.0388555 kg. The converter rounds the visible result to 68.0389 kg. For a form that asks for whole kilograms, this rounds to 68 kg. Keep extra digits if you are combining weights, and round the final value once. Rounding every small item first can shift the total enough to matter near a limit."),
            ("Account for packaging on a parcel", "If a place that accepts parcels lists a kilogram limit, weigh the packed item and include the box, tape and padding. Converting the item weight alone can understate what will be accepted. Compare the unrounded reading with the limit or leave a margin when your scale shows only whole pounds. Confirm the carrier's rules before printing a label for a package near the threshold."),
            ("Identify the unit before multiplying", "An ordinary pound is the unit used by most household scales. The label lb usually refers to that pound. Ounces are smaller units and need to be converted to pounds first. For example, 8 ordinary ounces is 0.5 lb, which converts to about 0.2268 kg. Check the scale's displayed units if it switches between ounces, pounds and decimal pounds."),
            ("Use the precision appropriate for your task", "A unit conversion cannot determine how accurate a scale is. For comparison shopping or tracking a routine measurement, the displayed value is often sufficient. If you are working under an equipment load rating or another safety-critical limit, use the specified units, a suitable scale and the instructions from the manufacturer. Contact the responsible professional when the limit cannot be verified."),
        }, new FaqItem[]
        {
            new("What is 150 lb in kilograms?", "About 68.0389 kg."),
            new("How do I convert kilograms to pounds?", "Divide kilograms by 0.45359237 or enter a value in the kg field."),
            new("Does lb mean pounds?", "Yes. The tool uses ordinary pounds, the unit used by household scales."),
            new("How many pounds is one kilogram?", "About 2.2046 lb."),
            new("Can I enter a fractional pound?", "Yes. For half a pound, type 0.5."),
            new("Does this determine a parcel's shipping weight?", "It converts the number you enter. Check the carrier's weighing and rounding rules."),
        }),
        ["miles-to-km"] = (new (string, string)[]
        {
            ("Convert miles to km using the road-distance mile", "Use Miles to KM to compare running routes, road signs and map directions that use different distance units. One ordinary mile equals exactly 1.609344 kilometers. Multiply miles by 1.609344 to find kilometers. Divide kilometers by the same factor to find miles. The kilometre spelling used in many countries and the US spelling kilometer refer to the same unit."),
            ("Check a 10-mile route", "A 10-mile route covers 16.09344 km. The converter displays 16.0934 km with four decimal places. In the other direction, a 5 km route is about 3.1069 miles. The displayed length says how far you will travel. It gives no travel time without information about your speed, stops and the conditions on the route."),
            ("Distinguish distance from a pace or speed", "If a route is 5 km, enter 5 in the kilometers field. A pace is minutes per unit of distance, so converting a pace uses the distance factor in the opposite direction from converting speed. For example, a pace of 10 minutes per mile is about 6.21 minutes per kilometer. Remember that 0.21 minutes is about 13 seconds, so the pace is roughly 6 minutes 13 seconds per kilometer."),
            ("Use the correct kind of mile", "This converter uses ordinary land miles. A nautical mile is a separate unit used for navigation on water and in aviation. Check the map legend, chart or instrument before converting a route described as a number of miles. A hiking app also may report straight-line distance or the length of a walking route. Conversion preserves the distance you entered, whichever method produced it."),
            ("Round once for planning", "Keep the original route distance if you are adding stages. Rounding each leg to a whole kilometer can shift the final total. When sharing directions, choose a precision that matches the trip. An approximate distance in whole kilometers can be enough for a road trip, while shorter routes or training intervals may need a decimal place. Record the original unit when you save the distance."),
        }, new FaqItem[]
        {
            new("How many kilometers is 10 miles?", "Exactly 16.09344 km, displayed as 16.0934 km."),
            new("How many miles is 5 km?", "About 3.1069 miles."),
            new("Is one mile exactly 1.6 km?", "1.6 is a rough estimate. One ordinary mile equals exactly 1.609344 km."),
            new("Does this tool convert nautical miles?", "It uses ordinary land miles."),
            new("Does it calculate travel time?", "It converts distance. Travel time depends on speed and stops."),
            new("Can I type kilometers first?", "Yes. Edit the kilometers field to calculate miles."),
        }),
        ["fahrenheit-to-celsius"] = (new (string, string)[]
        {
            ("Convert Fahrenheit to Celsius in the right order", "Use Fahrenheit to Celsius when you have an oven setting, weather forecast or thermometer reading on the other scale. Start by subtracting 32 from Fahrenheit. Then divide by 1.8, which is equivalent to multiplying by five ninths. To go back to Fahrenheit, multiply Celsius by 1.8 and add 32. Type in whichever field contains the reading you already know."),
            ("Work out 68°F without a table", "Subtract 32 from 68 to get 36. Divide 36 by 1.8 to get 20°C. To check it, multiply 20 by 1.8 for 36 and add 32 to return to 68°F. The subtraction matters because the scales have different zero points. Simply dividing 68 by 1.8 would ignore the offset and produce a different number."),
            ("Convert a change without using the offset", "A change of 18°F represents a change of 10°C. Divide the change by 1.8 without subtracting 32, because the offset cancels when you take the difference of two readings. For example, a rise from 50°F to 68°F is 18°F. Those readings convert to 10°C and 20°C, which confirms a difference of 10°C."),
            ("Set an oven in the increments it offers", "A recipe at 350°F converts to about 176.67°C. An oven that accepts only 5°C steps cannot be set to that exact reading. Choose the closest setting that suits the recipe, and check how the food is cooking. The converter does not account for fan settings, differences between ovens or temperature fluctuations around the set point."),
            ("Use negative readings and check the context", "Negative numbers are supported. At minus 40, the two scales show the same numerical value. The result rounds to two decimals for typical readings. When converting a weather forecast, the nearest degree is often enough. If the reading is being used for a safety threshold or a procedure, check the required units and use the measurement precision specified for it. If a chart lists a permitted range, convert both endpoints before comparing your reading with that range. Round only when you report the result."),
        }, new FaqItem[]
        {
            new("What is 68°F in Celsius?", "68°F equals 20°C."),
            new("What is 350°F in Celsius?", "About 176.67°C."),
            new("Why subtract 32 before dividing?", "Fahrenheit and Celsius have different zero points."),
            new("Where are the two scales equal?", "At minus 40, the Fahrenheit and Celsius values match."),
            new("How do I convert a rise of 18°F?", "Divide the difference by 1.8 to get a 10°C rise."),
            new("Can I type Celsius to get Fahrenheit?", "Yes. Enter a reading in the Celsius field."),
        }),
        ["pdf-to-powerpoint"] = (new (string, string)[]
        {
            ("Convert PDF to PowerPoint as image slides", "Use PDF to PowerPoint when you want to show pages from a report, worksheet or handout in a presentation. Each PDF page is rendered as an image and placed on its own widescreen slide. The converter creates a PPTX with the source pages in order. Choose a PDF and select Create PowerPoint, then download the deck when it is ready."),
            ("See how page count affects the deck", "A six-page handout produces six slides. The first slide contains page one and the sixth contains page six. Any blank pages in the PDF also get a slide. If you want slides only for pages two through four, extract those three pages with Split PDF first. Convert the extracted document to get three slides in the order they appear in the handout."),
            ("Keep the layout and plan around image text", "This method preserves the visible page as a picture. In a slide editor, you can move or resize that picture and add a new caption alongside it. Text and charts inside the picture stay part of the image. For a project that needs editable paragraphs, charts or tables, obtain the original presentation or use a conversion workflow that reconstructs the editable elements."),
            ("Check portrait pages on a widescreen slide", "The converter fits each image inside the slide while preserving its proportions. A portrait page will leave empty space at the sides. That space can hold a speaker note rendered as visible text, a callout or a second image that you add later in PowerPoint. Zoom into small text before presenting. A densely packed report may be easier to read if you dedicate separate slides to its key figures."),
            ("Work in short sections for large files", "The tool accepts one PDF up to 30 MB and 20 pages. Processing runs in the browser, and the original PDF stays on your device. If a file has more pages, use Split PDF to make smaller sections. Open each exported deck and check page order, margins and readability before combining the decks or sharing the slides."),
        }, new FaqItem[]
        {
            new("Are text and charts editable after conversion?", "Each PDF page is one image. Add separate editable text or charts in PowerPoint if needed."),
            new("How many slides will I get?", "The converter creates one slide for each PDF page in the source."),
            new("Does a scanned PDF work?", "A readable scan can be rendered as an image slide. The tool does not run OCR."),
            new("Will a portrait page be stretched?", "The image keeps its proportions and is centered on the widescreen slide."),
            new("Can I convert more than 20 pages?", "Split a longer document into sections, then convert each section."),
            new("Do my files leave my browser?", "The browser creates the PPTX locally."),
        }),
        ["rotate-pdf"] = (new (string, string)[]
        {
            ("Rotate PDF pages you want to keep upright", "Use Rotate PDF when scanned pages open sideways or upside down. Choose a PDF, pick the turn direction and enter the original page positions to change. Leave Pages blank if the whole file needs the same turn. Select Create PDF to save a separate copy with those rotations. The original remains available to try a different direction."),
            ("Fix three sideways pages in one pass", "Suppose an eight-page packet is upright except for pages 2, 3 and 8. If all three need a clockwise quarter-turn, enter 2-3,8 and choose 90 degrees clockwise. The new document still contains eight pages. Open it and check pages 2, 3 and 8. Use the half-turn option if you find a page that is upside down."),
            ("Use the direction from the current view", "Rotation adds a turn to the page's existing orientation. If you see the top of a page on the right edge of your screen, a counterclockwise turn brings the top back to the top. You can re-open the original PDF and calculate again if you pick the wrong direction. For pages that need different turns, apply one first and use that result for the second set."),
            ("Know what stays inside the file", "The tool adjusts the saved page rotation in the PDF. It does not turn every page into a new picture. A page that already has selectable text can still have selectable text after rotation. Cropping and straightening are separate jobs. If the scan is tilted by a few degrees or has a dark margin, a right-angle rotation will not correct that problem."),
            ("Review the copy before sending it", "The browser processes PDFs up to 50 MB and 200 pages. Page positions start at one, even when a printed number on the page says something different. Double-check the actual opening orientation in another PDF viewer if the document is a final handout. For a digitally signed document, use an approved signing workflow after you make changes. Keep the unedited file for your records."),
        }, new FaqItem[]
        {
            new("How do I rotate every page?", "Leave Pages blank and choose a turn direction."),
            new("Can I rotate just page 3?", "Enter 3 in the Pages field."),
            new("What does 90 degrees clockwise mean?", "It gives each selected page a quarter-turn to the right."),
            new("Will pages change order?", "Rotation preserves the existing page order."),
            new("Does this straighten a slightly tilted scan?", "The available turns are 90, 180 and 270 degrees."),
            new("Is the original PDF overwritten?", "The tool creates a separate download."),
        }),
        ["remove-pdf-pages"] = (new (string, string)[]
        {
            ("Remove PDF pages by their original positions", "Use Remove PDF Pages when a scan includes duplicate sheets, a blank back page or an appendix you do not need to send. Choose the PDF and enter the positions you want to delete. The remaining sheets stay in the order they had in the original. The new file is a separate download, so you can return to the source later."),
            ("Remove several pages in a single edit", "From a seven-page packet, enter 2,5-6 to remove the second, fifth and sixth sheets. The result has pages 1, 3, 4 and 7 in that order, for a total of four pages. The numbers in the input refer to the source file. Enter all the positions at once so they do not shift between edits. Check the downloaded copy against your intended list."),
            ("Distinguish file positions from printed labels", "PDF page positions start at one and count every sheet, including a title sheet and any blanks. If the first printed page is preceded by a cover and a contents page, the first printed page is the third sheet in the file. Use the position shown by your viewer's page navigation and then inspect the result to confirm the correct sheet was removed."),
            ("When extraction is the simpler choice", "If you want to keep most of a long document, listing the handful of pages to delete is convenient. If you want only two pages out of a long packet, use Split PDF to select the pages to keep. Both workflows can create shorter files. The difference for you is which set of page numbers is easier to enter and review."),
            ("Check links, attachments and confidential content", "The browser can process one PDF up to 50 MB and 200 pages. It keeps at least one page in the result. After deletion, check bookmarks and links that might point to a removed page. For sensitive material, use an approved redaction and sanitization workflow. Deleting a visible page does not verify that related attachments, metadata or other stored information have been cleared."),
        }, new FaqItem[]
        {
            new("Can I delete pages 5 through 7?", "Yes. Enter 5-7 to remove those original page positions."),
            new("Can I delete nonconsecutive pages?", "Use a comma-separated list such as 2,5,8."),
            new("Does it reorder the pages I keep?", "The remaining pages keep their original order."),
            new("What happens if I select every page?", "The tool asks you to keep at least one page."),
            new("Can I use printed page numbers?", "Use the page position in the PDF, counting from the first sheet."),
            new("Is page removal sufficient for redaction?", "Use a verified redaction and sanitization workflow for sensitive material."),
        }),
        ["add-pdf-page-numbers"] = (new (string, string)[]
        {
            ("Add PDF page numbers after arranging your document", "Use Add PDF Page Numbers to give a merged handout or report a visible footer. Choose the PDF, select the first page to label and set the number that should appear on it. You can place the number at the bottom center or bottom right. The tool counts up by one on every later page and gives you a new PDF to download."),
            ("Skip a cover and table of contents", "Suppose a six-sheet document has a cover and a contents page. Set First PDF page to label to 3 and Number to print there to 1. The first two sheets remain unnumbered. The remaining four show 1, 2, 3 and 4. If the document continues an earlier volume and the first labeled page should read 17, set the starting number to 17 instead."),
            ("Identify which number you are changing", "The starting page tells the tool where to put the first footer. The starting number tells it what text to draw there. Your reader's PDF viewer may still count the front sheets when it reports the current page. For example, the PDF viewer can say page 3 while the printed footer says 1. Specify which numbering scheme you mean when you refer to a page in an email."),
            ("Choose a footer with enough space", "Check whether body text, footnotes or an existing footer already occupy the lower margin. If an old footer is centered and the right corner is open, try placing the new number on the right. The tool overlays numbers and cannot automatically reserve space or detect old labels. Review the result at normal size on both portrait and landscape pages before distributing it."),
            ("Apply numbers near the end of your edits", "Arrange and rotate pages before numbering the final copy. If you remove pages afterward, footer numbers already drawn into the pages will not update. Run the numbering tool again from an unnumbered source if the order changes. This tool processes one PDF in the browser, with a 200-page and 50 MB cap. Keep the unnumbered original as a template."),
        }, new FaqItem[]
        {
            new("Can I skip my cover page?", "Set the first labeled page to 2 and use the number you want printed there."),
            new("Can the first label be a number other than 1?", "Yes. Enter a starting number from 1 to 9,999."),
            new("Where can I put the numbers?", "Choose bottom center or bottom right."),
            new("Will existing footer numbers be removed?", "The tool adds new footers. Existing content stays in place."),
            new("Does this change what my PDF viewer calls page 1?", "It adds visible numbers to the pages rather than changing the viewer's page labels."),
            new("Should I merge documents before numbering?", "Yes, if you want one continuous sequence through the combined file."),
        }),
        ["watermark-pdf"] = (new (string, string)[]
        {
            ("Watermark PDF pages with a text label", "Use Watermark PDF to mark a review copy with a status such as DRAFT or SAMPLE. Choose the PDF and enter the label. Set the opacity and list the pages you want to mark. Leave Pages blank for the entire document. The tool places the label across the center of each selected page and saves a new downloadable PDF."),
            ("Mark three sheets for internal review", "In a seven-page report, enter 2-4 to mark pages 2, 3 and 4. Set the label to DRAFT and try the default 25% opacity. Three sheets get the label, with the first and final three sheets unchanged. Open the output on a representative page before sharing it. Lower the opacity if the center text is hard to read, or increase it if the label is too faint."),
            ("Understand what opacity changes", "Opacity changes how strongly the color of the label is drawn over the content under it. A 25% label is more transparent than the same label at 60%. Readability also depends on whether it covers a white page, a photo or a table. Check the printed copy if recipients will review it on paper, since the on-screen result can look different from the printout."),
            ("Choose short words that work on different pages", "The text field accepts up to 80 characters using supported Latin letters, numbers and common punctuation. A short label usually fits both portrait and landscape pages better than a sentence. The tool centers and scales the text to fit the page's visible area. Use a separate note or cover sheet if people need a detailed explanation of the document's status."),
            ("Keep the version without the watermark", "Save the original for your final copy. Running the tool again on the marked version adds another overlay. A watermark tells a reader how to treat the document and has no role in access control. Use a secure sharing workflow when access must be limited. The browser handles one PDF up to 50 MB and 200 pages."),
        }, new FaqItem[]
        {
            new("How do I mark every page DRAFT?", "Enter DRAFT and leave Pages blank."),
            new("Can I watermark only selected pages?", "Enter original page positions or ranges, such as 2-4."),
            new("What opacity should I use?", "Start with 25% and check the label and underlying text on the downloaded copy."),
            new("Can I use a logo as the watermark?", "This version adds a text label."),
            new("Does the watermark protect confidential content?", "Use access controls or verified redaction for that purpose."),
            new("Can I remove a watermark by running this tool again?", "Use the original unmarked copy if you need to export without the label."),
        }),
        ["tola-to-gram"] = (new (string, string)[]
        {
            ("What is a tola?", "The tola is a traditional unit of weight used across South Asia, including Pakistan, India, Nepal and Bangladesh, mostly to weigh gold and silver. The standard value is 11.664 grams, which equals 180 grains or three eighths of a troy ounce. Jewellers, goldsmiths and bullion dealers still quote bars, coins and prices per tola, so it helps to convert it quickly into the grams shown on a modern scale."),
            ("How to convert tola to grams", "To change tola into grams, multiply by 11.664. To change grams into tola, divide by 11.664. Type a number into either box above and the other box updates instantly. For example, 1 tola is 11.664 g, 2.5 tola is 29.16 g, 5 tola is 58.32 g and 10 tola is 116.64 g. Going the other way, 50 g is about 4.287 tola and 100 g is about 8.573 tola."),
            ("Masha and ratti", "Smaller traditional units sit inside the tola. One tola is 12 masha, and one masha is 8 ratti, so one tola is 96 ratti. In metric terms a masha is about 0.972 g and a ratti is about 0.1215 g. Older shops may still weigh small pieces of jewellery in masha and ratti, so these ratios are handy when you compare a receipt with a digital scale."),
            ("Buying and selling gold", "Gold is often priced per tola or per 10 grams, and the price changes every day, so check the current rate with a trusted jeweller. Weight is only part of the story. Purity matters too: 24 karat gold is about 99.9% pure, while 22 karat is about 91.7% pure. Making charges and wastage deductions are separate from the metal price. Always ask for a written receipt that lists the weight, karat and charges, and make sure the scale has been checked."),
            ("Gold and silver nisab", "Many people use tola when they calculate zakat. A commonly cited nisab is 7.5 tola of gold (87.48 g) or 52.5 tola of silver (612.36 g). Scholars and schools of thought can differ on the exact method, so confirm the figures that apply to you with a qualified scholar. This tool only converts weights and does not calculate zakat or show prices."),
            ("Regional differences", "This converter uses the widely accepted value of 11.664 grams. A few regions and older documents have used slightly different tola weights, and the old Indian government tola was commonly given as 11.66 grams. If you are working from an old deed or a historical record, check which definition it used before you rely on the result."),
        }, new FaqItem[]
        {
            new("How many grams are in one tola?", "One tola is 11.664 grams."),
            new("How many tola are in 10 grams?", "10 grams is about 0.857 tola."),
            new("How many tola are in 100 grams?", "100 grams is about 8.573 tola."),
            new("How many ounces is one tola?", "One tola is about 0.411 ounces, or exactly three eighths of a troy ounce."),
            new("Is the tola used for silver as well as gold?", "Yes. Gold and silver are both commonly weighed and sold by the tola in South Asia."),
            new("Does this tool show the gold price?", "No. It only converts weight. Ask a local jeweller for the current price per tola."),
        }),
        ["compress-image"] = (new (string, string)[]
        {
            ("Why reduce an image's file size", "Many websites, government forms and job portals only accept images below a certain size, often 50 KB, 100 KB or 200 KB. Email services and messaging apps also limit attachments, and smaller images load faster on web pages. This tool shrinks a photo to a size you choose so it passes those limits without needing any software."),
            ("How to compress an image to a target size", "Click Choose file, pick a JPG, PNG or WebP image, enter the size you want in KB, then press Compress. The tool lowers the picture quality step by step until the file fits under your target. If the quality alone is not enough, it also reduces the pixel dimensions a little. When it finishes you see the before and after sizes and a link to download the compressed JPEG."),
            ("What happens to the picture", "Compression removes some fine detail that the eye barely notices, which is why a photo can drop to a fraction of its original size. Very small targets, for example 20 KB for a large photo, will make the image look softer or blocky. For the best result start from the original photo, not from a copy that has already been compressed several times, because each round of compression loses more detail."),
            ("Tips for better results", "Choose a target that is realistic for the image. A phone photo of 4 MB can usually reach 200 KB with little visible change, while 30 KB will be noticeably softer. Photos compress better than screenshots with lots of text, because sharp text edges show compression marks quickly. If you need a particular width and height as well, resize the image first with our resize tool and then compress it."),
            ("Formats and transparency", "The output is always a JPEG, which is the best format for photos and gives small files. If your original is a PNG with a transparent background, the transparent areas are filled with white because JPEG cannot store transparency. Keep your original PNG if you need to preserve a transparent background."),
            ("Privacy", "The compression runs entirely in your browser. Your image is not uploaded to our server, which makes this safe for documents such as ID photos and signatures. Close the page and nothing is kept."),
        }, new FaqItem[]
        {
            new("How do I reduce an image to 100 KB?", "Choose your image, type 100 in the target size box and press Compress. The tool aims just under 100 KB."),
            new("Will the quality get worse?", "Some detail is lost, more so for smaller targets. A modest reduction is usually hard to see."),
            new("Why is my result smaller than the target?", "The tool picks the highest quality that still fits, so the final size can be a little below your number."),
            new("Can I compress a PNG or WebP image?", "Yes. PNG, JPG and WebP files are accepted and the result is saved as a JPEG."),
            new("Is there a limit on the original file size?", "Very large photos work, but extremely large files may take longer on older phones."),
            new("Are my pictures uploaded anywhere?", "No. Everything happens inside your browser."),
        }),
        ["resize-image"] = (new (string, string)[]
        {
            ("Resize an image by pixels", "Choose a picture, then type the width or height you need in pixels. If Keep aspect ratio is ticked, the other number changes automatically so the picture is not stretched. Pick JPEG for photos or PNG when you need lossless quality, press Resize and download the result. The original file is never changed."),
            ("Pixels, not file size", "Resizing changes the number of pixels in the image, such as 1920 by 1080. A smaller pixel size normally gives a smaller file, but it is not the same as compression. If a form asks for a limit in kilobytes, resize first and then use the compress tool to hit the exact size."),
            ("Common image sizes", "A few sizes people use often: 1080 x 1080 for a square social post, 1080 x 1920 for stories and reels, 1920 x 1080 for full HD screens and video thumbnails, 1200 x 630 for link previews on social media, and 600 x 600 for a typical passport or visa photo. Always check the exact size that your website or form asks for."),
            ("Making an image larger", "You can enter a bigger size than the original, but extra pixels cannot add real detail. Enlarged images look soft or blurry, so try to start from the largest original you have. Shrinking is always safer than enlarging."),
            ("Aspect ratio", "Aspect ratio is the relationship between width and height. A 4:3 photo resized to a square will look squeezed unless you crop it first. Keep the lock on to avoid distortion, and untick it only when you really want to stretch the image to exact dimensions."),
            ("Privacy and limits", "Resizing runs in your browser, so your picture does not leave your device. The tool accepts sizes from 1 to 8000 pixels on each side to keep your browser stable."),
        }, new FaqItem[]
        {
            new("How do I resize an image without losing quality?", "Shrink it rather than enlarge it, keep the aspect ratio lock on and save as PNG or high quality JPEG."),
            new("How do I resize to exactly 800 x 600?", "Untick Keep aspect ratio, type 800 and 600, and press Resize. The picture may stretch if the original has a different ratio."),
            new("What is the difference between resizing and compressing?", "Resizing changes the pixel dimensions. Compressing reduces file size, usually by lowering quality."),
            new("Does resizing change my original?", "No. A new file is created and your original stays as it was."),
            new("Which format should I choose?", "JPEG for photos and smaller files, PNG for graphics or when you need lossless quality."),
            new("Are my images uploaded?", "No. The tool works entirely in your browser."),
        }),
        ["jpg-to-pdf"] = (new (string, string)[]
        {
            ("Turn photos into one PDF", "Select one or more JPG or PNG images, choose A4 or US Letter, and press Convert. Each image is placed on its own page, scaled to fit with a small margin and kept in the order you picked them. The finished PDF downloads in a few seconds. It is a quick way to combine scanned pages, receipts, certificates or ID copies into a single document you can email or upload."),
            ("Tips for scanned documents", "Take photos in good light, hold the phone straight above the page and keep the whole page in view. Crop away the background before converting, and choose A4 for most of the world or US Letter for the United States and Canada. If you want the pages in a certain order, select the files in that order or rename them 1, 2, 3 first."),
            ("Keeping the PDF small", "Large camera photos create large PDFs. If you need to stay under an upload limit, reduce the images first using our compress image tool, then convert them. Aim for a few hundred kilobytes per page for ordinary text documents."),
            ("Limits and privacy", "You can convert up to 10 images at a time, each up to 10 MB. Files are processed in memory to build the PDF and are not stored on our server after the result is created. The free version has a small daily limit so the service stays available for everyone."),
            ("Supported formats", "JPG, JPEG and PNG images work. If you have HEIC photos from an iPhone, convert them with our HEIC to JPG tool first. Transparent PNG areas appear white or as the page color, depending on the viewer."),
            ("After you convert", "Open the PDF to check the page order and orientation before you send it. Landscape photos are placed on landscape pages automatically. If you need to join this PDF to other documents, use the merge PDF tool."),
        }, new FaqItem[]
        {
            new("Is the JPG to PDF converter free?", "Yes. Free use has a small daily limit."),
            new("Are my images stored on your server?", "No. They are processed in memory and discarded once the PDF is made."),
            new("Can I put several images in one PDF?", "Yes. Select up to 10 images and each becomes a page."),
            new("What page sizes are available?", "A4 and US Letter."),
            new("How do I make the PDF smaller?", "Compress or resize the images before converting."),
            new("Does it work on a phone?", "Yes. It works in any modern mobile browser."),
        }),
        ["merge-pdf"] = (new (string, string)[]
        {
            ("Combine PDF files into one", "Click Choose file and add two or more PDF documents. Use the Up and Down buttons to set the order, remove any file you added by mistake, then press Merge PDFs. A single combined PDF downloads straight away. This is useful for assembling an application pack, joining invoices, or putting scanned chapters back together."),
            ("How it works", "The tool copies every page from each of your files, in order, into a new PDF. Your original files are not changed. Because everything happens in your browser, the documents are not uploaded to a server, which is important for contracts, IDs and financial paperwork."),
            ("Things to check", "Password-protected or damaged PDFs cannot be merged, so unlock them first. Page sizes stay as they are, so a mix of A4 and Letter pages will remain mixed. Bookmarks, form fields and some interactive elements may not carry over, so check the result before you rely on it."),
            ("Keeping the file size down", "A merged PDF is roughly as large as all the parts added together. If the result is too big for an email or upload form, try compressing the images in the source documents before merging, or send it as separate files."),
            ("Limits", "For best performance keep the total size under about 100 MB. Very large merges depend on the memory of your device, and phones may struggle with big files."),
            ("Merge, then split", "If you only need some pages from a long document, use the split PDF tool to extract them first, and then merge the extracted parts. This keeps the final file smaller and avoids sending pages you do not need."),
        }, new FaqItem[]
        {
            new("How do I merge PDF files?", "Add your PDFs, put them in order with the arrows and press Merge PDFs."),
            new("Can I change the order of the files?", "Yes. Use the Up and Down buttons next to each file."),
            new("Are my PDFs uploaded?", "No. They are combined in your browser."),
            new("Can I merge password-protected PDFs?", "Not until they are unlocked."),
            new("Is there a page limit?", "No fixed limit, but a total size over about 100 MB may be slow."),
            new("Will the quality change?", "No. Pages are copied as they are, without re-compressing."),
        }),
        ["loan-calculator"] = (new (string, string)[]
        {
            ("How the loan calculator works", "Enter the amount you want to borrow, the yearly interest rate (APR) and the length of the loan in years. The calculator uses the standard fixed-rate amortization formula to find your monthly payment, the total interest you would pay, and the total amount repaid. The formula is payment = P x r / (1 - (1 + r)^-n), where P is the loan, r is the monthly rate (APR divided by 12) and n is the number of monthly payments."),
            ("A worked example", "Borrowing $20,000 at 7% APR over 5 years gives a monthly payment of about $396.02. Over 60 payments you repay about $23,761, so the interest is about $3,761. Stretch the same loan to 7 years and the payment falls to about $301.85, but total interest rises to about $5,356. Shorten it to 3 years and the payment is about $617.54 with only about $2,232 of interest."),
            ("Term versus total cost", "A longer term lowers each monthly payment, which can make a loan easier to fit in your budget, but you pay interest for more months. A shorter term costs more per month and much less overall. Compare the total repaid, not only the monthly figure, when you choose between offers."),
            ("APR and fees", "The APR is the yearly rate the lender charges on the balance. Some loans also add an origination fee, insurance or other charges that are not included here, so a lender's quote can be higher. Ask for the total cost and the APR including fees before you sign anything."),
            ("Paying off early", "Extra payments go straight to the balance, which saves interest and shortens the loan. Check whether your lender charges a prepayment penalty. You can test the effect by re-running the calculator with a shorter term."),
            ("Limits of this estimate", "This tool assumes a fixed rate and equal monthly payments. It does not cover variable rates, interest-only periods, balloon payments or taxes. Treat the numbers as a planning guide and not as a loan offer or financial advice."),
        }, new FaqItem[]
        {
            new("How is a monthly loan payment calculated?", "With payment = P x r / (1 - (1 + r)^-n), using the monthly rate and the number of months."),
            new("What is the monthly payment on a $20,000 loan at 7% for 5 years?", "About $396.02 per month."),
            new("Does a longer term save money?", "It lowers the monthly payment but increases the total interest you pay."),
            new("What is the difference between interest rate and APR?", "APR includes certain fees as well as the interest rate, so it shows the cost of borrowing more fully."),
            new("Can I use this for a personal, student or business loan?", "Yes, for any fixed-rate loan with equal monthly payments."),
            new("Is this financial advice?", "No. It is an estimate for planning. Ask your lender for an official quote."),
        }),
        ["age-calculator"] = (new (string, string)[]
        {
            ("Find your exact age", "Enter a date of birth and, if you like, a different date to measure to. The calculator shows your age in complete years, months and days, the total number of days you have lived, and how many days remain until your next birthday. The second date starts as today, so one click gives you your age right now."),
            ("How the calculation works", "The tool counts full years first, then the remaining months, then the remaining days. When the day number of the end date is smaller than the birth day, it borrows days from the previous month, exactly as you would on a calendar. For example, someone born on 29 February 2000 is 24 years, 0 months and 1 day old on 1 March 2024."),
            ("Leap years and leap-day birthdays", "Leap years are included automatically, which makes the total-days figure accurate. People born on 29 February celebrate on 28 February or 1 March in other years, depending on local custom and law, so check the rule for official documents."),
            ("Age on a past or future date", "Change the second date to find your age at any moment. This is useful for checking whether you will meet an age requirement on a certain date, for school admission cut-off dates, or for working out how old someone was when an event happened."),
            ("Why exact age matters", "Applications for jobs, exams, insurance, sports and visas often set strict age limits that count in years, months and sometimes days. Counting by hand across different month lengths is easy to get wrong, so a calculator saves mistakes."),
            ("Privacy", "The dates you enter are used only in your browser and are never sent anywhere."),
        }, new FaqItem[]
        {
            new("How do I calculate my age in years, months and days?", "Enter your date of birth and press Calculate age. The result shows all three."),
            new("How many days old am I?", "The tool shows the total number of days between your birth date and the second date."),
            new("Can I find my age on a future date?", "Yes. Set the second date to the date you want."),
            new("How are leap years handled?", "Real calendar dates are used, so leap days are counted correctly."),
            new("When is my next birthday?", "The result shows how many days remain until your next birthday."),
            new("Is my birth date stored?", "No. Everything stays in your browser."),
        }),
        ["percentage-calculator"] = (new (string, string)[]
        {
            ("Three percentage tools in one", "This page answers the three most common percentage questions. The first box finds a percentage of a number, such as 15% of 80. The second finds what percent one number is of another. The third finds the percentage change between an old value and a new value. Fill in the numbers and press Calculate in the section you need."),
            ("Formulas and examples", "Percent of: multiply the number by the percent and divide by 100, so 15% of 80 is 12. What percent: divide the part by the whole and multiply by 100, so 12 is 15% of 80. Percentage change: subtract the old value from the new one, divide by the old value and multiply by 100, so going from 50 to 75 is a 50% increase."),
            ("Increase and decrease are not symmetrical", "A rise from 50 to 75 is +50%, but a fall from 75 back to 50 is only -33.33%. The percentage is always measured against the starting value, and the starting value is different in each direction. This is why a 20% drop followed by a 20% rise does not bring you back to where you began."),
            ("Discounts and sales", "To find a sale price, work out the discount and subtract it. A 20% discount on $85 is $17, so you pay $68. For a quick check, 10% is easy: move the decimal point one place to the left. Then double it for 20% or halve it for 5%."),
            ("Percent and percentage points", "If a rate rises from 4% to 6%, it has gone up by 2 percentage points, which is a 50% increase in the rate itself. News reports often mix the two, so check which one is meant."),
            ("Rounding", "Results are shown with up to four decimal places. If you need a rounded figure for a bill or report, round the final answer rather than the numbers you type in."),
        }, new FaqItem[]
        {
            new("How do I calculate X percent of a number?", "Multiply the number by X and divide by 100. For example, 15% of 80 is 12."),
            new("How do I find what percent one number is of another?", "Divide the first number by the second and multiply by 100."),
            new("How do I calculate percentage increase?", "Subtract the old value from the new value, divide by the old value and multiply by 100."),
            new("Why can't I calculate a percentage change from zero?", "The formula divides by the old value, and dividing by zero is undefined."),
            new("How do I calculate a discount?", "Find the percentage of the price, then subtract it. 20% off $85 is $17 off, so you pay $68."),
            new("What is the difference between percent and percentage points?", "A move from 4% to 6% is 2 percentage points, or a 50% relative increase."),
        }),
        ["word-counter"] = (new (string, string)[]
        {
            ("Count words as you type", "Type or paste any text into the box and the counts update instantly. The tool shows the number of words, characters with and without spaces, sentences, paragraphs and an estimated reading time. There is no button to press and nothing to install."),
            ("How the counts are made", "A word is any group of characters separated by spaces or line breaks. Sentences are counted by the full stops, question marks and exclamation marks that end them. A paragraph is a block of text separated by a blank line. Reading time is based on about 200 words per minute, a typical pace for silent reading. Reading aloud is slower, often around 130 words per minute."),
            ("Handy limits to know", "Many places set length limits. A post on X (Twitter) allows 280 characters. SEO page titles work best at around 60 characters and meta descriptions at about 155. Many school essays, scholarship applications and job forms set word counts, and a short bio or product description often needs to stay under 150 or 200 words."),
            ("Using it for writing", "Check the length of an essay against your word limit, trim an introduction that runs long, or estimate how long a speech will take. Pasting a paragraph at a time also shows where your sentences are getting long, since a very high words-per-sentence average usually means the writing is hard to read."),
            ("Characters with and without spaces", "Some limits count spaces and some do not, so both numbers are shown. Line breaks count as characters in the first number and are excluded from the second."),
            ("Privacy", "Your text stays in your browser. It is not saved and not sent to any server, so you can safely check private drafts."),
        }, new FaqItem[]
        {
            new("How do I count words in a document?", "Paste the text into the box and the word count appears immediately."),
            new("How is reading time calculated?", "It uses an average speed of 200 words per minute."),
            new("How many characters can I use in a tweet?", "280 characters on X (Twitter) for a standard post."),
            new("How long should an SEO title and meta description be?", "Around 60 characters for the title and 155 for the description."),
            new("Does it count punctuation and spaces?", "The first character figure includes spaces and punctuation. The second figure leaves spaces out."),
            new("Is my text saved anywhere?", "No. Everything happens in your browser."),
        }),
        ["grade-calculator"] = (new (string, string)[]
        {
            ("Calculate a weighted course grade", "Most courses weigh assignments differently: homework might be 20% of your grade, a midterm 30% and the final exam 40%. List each graded item with your percentage score and its weight. The calculator multiplies each score by its weight, adds the results and divides by the total weight to give your overall grade. Use Add row for more items and Remove for items you do not need."),
            ("A worked example", "Homework 92% (weight 20), Midterm 84% (weight 30) and Project 88% (weight 20) gives (92 x 20 + 84 x 30 + 88 x 20) / 70 = 87.43%. Because the weights add up to only 70, this is your average so far, not your final grade. The tool shows the total weight so you can see how much of the course is still to come."),
            ("What grade do I need on the final exam?", "The second box works backwards. Enter your current grade, the weight of the final exam and the grade you want. The formula is needed score = (target - current x (1 - weight)) / weight. For example, with a current grade of 88%, a final worth 25% and a target of 90%, you need (90 - 66) / 0.25 = 96% on the final. If the answer is above 100%, the target is out of reach without extra credit; if it is below 0%, you have already secured it."),
            ("Tips for using the results", "Use percentages rather than letter grades, and check your syllabus for how your teacher rounds and which grade boundaries apply, since A, B and C cut-offs differ between schools. If your teacher drops the lowest quiz score, leave that score out. Extra credit can push a category above 100%, and you can enter it as a score over 100."),
            ("Weighted versus simple average", "A simple average treats every item equally, which is only right when everything has the same weight. A weighted average counts bigger assignments more, so a low exam score can pull your grade down much more than a low homework score."),
            ("Limits", "This tool gives an estimate to help you plan. It does not calculate GPA, and the official grade is always the one your school records."),
        }, new FaqItem[]
        {
            new("How do I calculate a weighted grade?", "Multiply each score by its weight, add the results and divide by the total of the weights."),
            new("Do the weights have to add up to 100%?", "No. The tool divides by the total you enter and warns you if it is not 100%."),
            new("How do I find the score I need on my final exam?", "Use the final exam box with your current grade, the final's weight and your target grade."),
            new("What if the needed score is above 100%?", "The target is not reachable without extra credit."),
            new("Can I use letter grades?", "Convert them to percentages first, using your school's scale."),
            new("Does this calculate GPA?", "No. It calculates a course grade only."),
        }),
    };
}
