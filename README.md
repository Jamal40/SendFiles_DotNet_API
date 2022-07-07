# Sending Files in .Net Core API Guide

## Purpose:
This demo has been made to demonstrate how to submit a form that includes a photo using Web API.

## Approach:
Typically There are three ways to handle this scenario:

  1- Sending all the data using content-type: multipart/form-data.
  
  2- Sending all the data using content-type: application/json and using base64 to handle images.
  
  3- Sending multiple requests, one to handle the image using multipart/form-data and responds with the image URL, and the other to handle the form submission using application/json. The approach used in the demo is this one. 

## Flow:
The form will be disabled until the user uploads a photo, once the photo is uploaded, a request is sent to the server and the server responds with the URL, the user will be able to submit the form as long as it's valid.

## Structure:
FilesController: contains the central endpoint for the whole application to handle files/images. Given an image, the endpoint will return a URL.
ProductsController: contains a dummy endpoint to mimic the product with image submission.

## Authors
Special Thanks to:
  - [@Bassant](https://github.com/Bassanthebashi)
  - [@Yasmeen](https://github.com/yasminomar)
