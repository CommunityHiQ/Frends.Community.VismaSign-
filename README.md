# Frends.Community.VismaSign
Frends tasks to use VismaSign API: https://sign.visma.net/api/docs/v1/
***

- [Frends.Community.VismaSign](#frendsjson)
  - [Installing](#installing)
  - [Building](#building)
  - [Contributing](#contributing)
  - [Documentation](#documentation)
    - [VismaSign.DocumentCreate](#documentcreate)
      - [Input](#input)
      - [Options](#options)
      - [Result](#result)
    - [VismaSign.DocumentAddFile](#documentaddfile)
      - [Input](#input-1)
      - [Options](#options-1)
      - [Result](#result-1)
    - [VismaSign.DocumentAddInvitations](#documentaddinvitations)
      - [Input](#input-2)
      - [Options](#options-2)
      - [Result](#result-2)
    - [VismaSign.DocumentSearch](#documentsearch)
      - [Input](#input-3)
      - [Options](#options-3)
      - [Result](#result-3)
    - [VismaSign.DocumentGet](#documentget)
      - [Input](#input-4)
      - [Options](#options-4)
      - [Result](#result-4)
  - [License](#license)

# Frends.VismaSign

## Installing
You can install the task via FRENDS UI Task view, by searching for packages. You can also download the latest NuGet package from nuget feed and import it manually via the Task view.

## Building
Requirements

`NET Core SDK 2.1 or later`

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.VismaSign`

Restore dependencies

`dotnet restore`

Build the solution

`dotnet build`

Create a nuget package

`dotnet pack Frends.Community.VismaSign`

## Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

## Documentation

### VismaSign.DocumentCreate 

Create document. See https://sign.visma.net/api/docs/v1/#action-document-create

#### Input

| Property        | Type                          | Description                  | Example                  |
|-----------------|-------------------------------|------------------------------|--------------------------|
| Body            | json						  | Json body			         | `{"key":"value"}`        |

#### Options

| Property              | Type           	| Description                                    |
|-----------------------|-------------------|------------------------------------------------|
| Identifier    		| string           	| Client identifier |
| Secret     			| string           	| Client secret 	|
| BaseAddress   		| string           	| Base address		|
| ThrowExceptionOnErrorResponse	| bool           	| Throws error if request failed		|

#### Result

| Property          | Type                      | Description             |
|-------------------|---------------------------|-------------------------|
| Location          | string                    | Location of the created document |
| Headers           | Dictionary<string,string> | Response headers        |
| StatusCode        | int                       | Response status code    | 

### VismaSign.DocumentAddFile 

Add file. See https://sign.visma.net/api/docs/v1/#action-document-add-file

#### Input

| Property        | Type                          | Description                  | Example                  |
|-----------------|-------------------------------|------------------------------|--------------------------|
| DocumentUriId          | string						  | Document uri			         | `e59c8dc8-8848-4936-ac7c-50d9ed72085a`        |
| InputBytes	         | byte[]						  | Input bytes	 if !ReadFromFile	 |				|
| ReadFromFile			 | boolk						  | Choose to read data from file	 | `true`       |
| FileLocation           | string						  | Location of the pdf file if ReadFromFile	| `c:\test.pdf`        |
| FileName				 | string						  | (Optinal) add file name			 | `test.pdf`        |

#### Options

| Property              | Type           	| Description                                    |
|-----------------------|-------------------|------------------------------------------------|
| Identifier    		| string           	| Client identifier |
| Secret     			| string           	| Client secret 	|
| BaseAddress   		| string           	| Base address		|
| ThrowExceptionOnErrorResponse	| bool           	| Throws error if request failed		|

#### Result

| Property          | Type                      | Description             |
|-------------------|---------------------------|-------------------------|
| Body              | string                    | Response body as string |
| Headers           | Dictionary<string,string> | Response headers        |
| StatusCode        | int                       | Response status code    | 

### VismaSign.DocumentAddInvitations

Add invitation. See https://sign.visma.net/api/docs/v1/#action-document-create-invitations
		
#### Input

| Property        | Type                          | Description                  | Example                  |
|-----------------|-------------------------------|------------------------------|--------------------------|
| DocumentUriId   | string						  | Document uri		         | `e59c8dc8-8848-4936-ac7c-50d9ed72085a`        |
| Body            | json						  | Json body			         | `{"key":"value"}`        |

#### Options

| Property              | Type           	| Description                                    |
|-----------------------|-------------------|------------------------------------------------|
| Identifier    		| string           	| Client identifier |
| Secret     			| string           	| Client secret 	|
| BaseAddress   		| string           	| Base address		|
| ThrowExceptionOnErrorResponse	| bool           	| Throws error if request failed		|

#### Result

| Property          | Type                      | Description             |
|-------------------|---------------------------|-------------------------|
| Body              | string                    | Response body as string |
| Headers           | Dictionary<string,string> | Response headers        |
| StatusCode        | int                       | Response status code    | 

### VismaSign.DocumentSearch

Search document. See https://sign.visma.net/api/docs/v1/#search-documents

#### Input

| Property        | Type                          | Description                  | Example                  |
|-----------------|-------------------------------|------------------------------|--------------------------|
| Query            | string						  | query parameters	         | `uuid=e59c8dc8-8848-4936-ac7c-50d9ed72085a`        |

#### Options

| Property              | Type           	| Description                                    |
|-----------------------|-------------------|------------------------------------------------|
| Identifier    		| string           	| Client identifier |
| Secret     			| string           	| Client secret 	|
| BaseAddress   		| string           	| Base address		|
| ThrowExceptionOnErrorResponse	| bool           	| Throws error if request failed		|

#### Result

| Property          | Type                      | Description             |
|-------------------|---------------------------|-------------------------|
| Body              | string                    | Response body as string |
| Headers           | Dictionary<string,string> | Response headers        |
| StatusCode        | int                       | Response status code    | 

### VismaSign.DocumentGet

Get document. See https://sign.visma.net/api/docs/v1/#action-document-get-file

#### Input

| Property        | Type                          | Description                  | Example                  |
|-----------------|-------------------------------|------------------------------|--------------------------|
| DocumentUriId   | string						  | Document uri		         | `e59c8dc8-8848-4936-ac7c-50d9ed72085a`        |

#### Options

| Property              | Type           	| Description                                    |
|-----------------------|-------------------|------------------------------------------------|
| Identifier    		| string           	| Client identifier |
| Secret     			| string           	| Client secret 	|
| BaseAddress   		| string           	| Base address		|
| ThrowExceptionOnErrorResponse	| bool           	| Throws error if request failed		|

#### Result

| Property          | Type                      | Description             |
|-------------------|---------------------------|-------------------------|
| Body              | string                    | Response body as string |
| Headers           | Dictionary<string,string> | Response headers        |
| StatusCode        | int                       | Response status code    | 


## License
This project is licensed under the MIT License - see the LICENSE file for details
