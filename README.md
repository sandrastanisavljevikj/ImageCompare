# ImageCompare

To test the API, run it locally and please please use a tool as Postman to make a post request. In the body of the post request provide the names of 
front image scan, and possibly back image scan.

example: the body of the request i used, looks like:
{
	"frontImage": "tree.png",
	"backImage": "back-test.png"
}
and the url for the test post request is https://localhost:44369/api/compare, so the endpoint is /api/compare.

Update the use ssl setting if necessary: go to project properties -> Debug-> use SSL and update the url you call as well.

Make sure that the images are present in the wwwroot folder (this would potentialy be done automatically with a client part that allows upload option).

I you want to make your own tests update FrontReferenceImagePath and BackReferenceImagePath with the new names of the reference images. The reference images 
should also be present in the wwwroot folder.