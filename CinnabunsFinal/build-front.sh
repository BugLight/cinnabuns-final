#!/bin/bash

if [[ -z $BUILD_DOCKER ]]
then
echo -n "Building"
cd $ClientApp
npm install
npm run build
else
echo -n "No building"
fi
