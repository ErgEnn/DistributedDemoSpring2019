# Docker


Get the latest nginx image from docker hub
~~~
docker pull nginx
~~~


Build aurelia app
~~~
au build
~~~

Aurelia files fro your app are now in dist folder

Copy web content (aurelia) over to docker. Create Dockerfile for this
~~~
FROM nginx
COPY dist /usr/share/nginx/html
~~~

Build the image (yes there is . at the end of the command)
~~~
docker build -t nginxaurelia .
~~~

Make a test run
~~~
docker run --name nginx  --rm -it -p 8080:80 nginxaurelia
~~~

Browse to http://localhost:8080 and test that everything is working.
Don't forget your backend setings in Aurelia - it should point to remote Rest api endpoint.


Tag the image, log into docker from cli and push it to docker hub (use your own docker account!)
~~~
docker tag nginxaurelia <your_docker_username>/nginxaurelia:test
docker login -u <your_docker_username> -p <your_docker_password>
docker push <your_docker_username>/nginxaurelia:test
~~~

And then deploy it to hosting!
