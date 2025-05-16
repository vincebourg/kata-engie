# Kata

## Objectives

Check knowledge about front and back communications in .Net with a vue front-end.

## Context

You are working at an energy provider company and want to estimate building consumptions when heating during winter.

## Exercice 1

The website should contain a form that allows to give the properties of a building:
- ground area
- number of levels
- usage

These informations are sent to the back that uses a proprietary algoritm to calculate its consumption.

## Exercice 2

The website should provide an endpoint that returns a geojson file with building consumptions calculated.

The front will display the colors of the buildings according to the consumption.

## Proprietary algorithm for consumptions

ground area * number of levels * Ratio.

## Parameters

|Usage|Ratio in kWh/m²|
|--|--|
|Hospital|27|
|Housing|50|
|Shop|60|


