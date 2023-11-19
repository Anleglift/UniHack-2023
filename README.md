# UniHack-2023

## App Name: R&T-Recycling & Tracking

Short Description: The main objectives of our app, R&T - Recycling & Tracking, are to draw public attention to the danger caused by improper waste management by assisting users in proper waste disposal. Additionally, the app aims to highlight the number of times users have recycled and to provide them with a broader perspective by approximating the environmental impact of the app, gathering feedback from users worldwide.

## MLH GOOGLE CLOUD USE:

Google Cloud Use: We utilise the Google Maps API for our base map and on top of that we have added roughly 1430 markers corresponding to the all the locations in Romania where our users can go and recycle, afterwards being rewarded appropiately.The custom-made map is called upon from within Unity updating with their location (only for the user in question) whenever needed. This map can work for multiple users at a time with no problem arrising. 

## Tehnical Details:

### MAP:

How do we create our map?: Using the Google Maps API and HTML script we host our custom-made map on netlify on https://static-map-unihack2023.netlify.app/ which then inside Unity, whenever needed, the map is updated with the format https://static-map-unihack2023.netlify.app/?lat=x&lng=y, where x and y are the latitude and longitude of the user being sent from within Unity to the site, Unity then getting the updating map and showcasing it to the viewer with a WebViewElement.

### Status Checker:

How does the app know my status?: Using the geoLocation from within Unity we get the last 2 coordinates sets of the user and then compare the distance between the 2 sets afterwards calculating the speed and according to that we update the status of the User, rewarding them if they have a smaller carbon footprint (i.e. walking).

### Recycle Checker:

How does the app know if i have recycled?: Whenever the user is near a recycling location then it pressed an in-app button which then querries the known locations of the markers and checks if the user is within a 3m radius of the recycling location, if they are they are rewarded accordingly if not the user is informed and they can try again.
