# IllusionDatabase
## How to deploy?
```bash=
# 1. Install Yarn
npm install --global yarn

# 2. Install dependencies
yarn install

# 3. Download and extract gifs/icons into ./statics/[gifs|icons]

# 4. Import collections from /Database data

# 5. Start app
node app.js

```
## Entrypoints
* GET
    * [/illusions?extend=[true|false]](#GET-illusionsextendtruefalse)
    * [/illusions/:id](#GET-illusionsid)
    * [/tags/[elements|effects]?populate=[true|false]](#GET-tagselementseffectspopulatetruefalse)
* POST
    * [/illusions/search/\[elements|effects\]](#POST-illusionssearchelementseffects)
    * [/illusions/:name](#POST-illusionsname)
### GET /illusions?extend=\[true|false\]
#### Request
```
No need to send anything
```
#### Response
```javascript=
// Unextended
[
    {
        "_id": "60310098d77a41032a433b45",
        "title": "McCollough Effect"
    },
    // ...
]
// Extended
[
    {
        "elements": [
            "602d0e92771e4402dd6854d8",
            "602d0e92771e4402dd6854dd",
            "602d0e92771e4402dd6854ea",
            //...
        ],
        "effects": [
            "602d0e92771e4402dd685513",
            "602d0e92771e4402dd685517",
            "602d0e92771e4402dd68551e"
        ],
        "_id": "60310098d77a41032a433b45",
        "update_at": "2021-02-20T12:29:12.020Z",
        "name": "mccollough_effect",
        "content": "## Read this first\nIf you follow the instructions below, you will change your brain for a prolonged time (up to month), in addition to simply remembering this. Proceed only if this is ok with you.",
        "title": "McCollough Effect",
        "__v": 0
    }, 
    // ...
]
```
#### Description
Get all illusions.


---

### GET /illusions/:id
#### Request
```
No need to send anything
```
#### Response
```javascript=
{
    "elements": [
        {
            "_id": "603e53ea6a94d1071cb7b427",
            "name": "Motion",
            "iconURL": "/icons/Motion.PNG"
        },
        {
            "_id": "603e53ea6a94d1071cb7b42d",
            "name": "Color",
            "iconURL": "/icons/Color.PNG"
        },
        ...
    ],
    "effects": [
        {
            "_id": "603e53ea6a94d1071cb7b465",
            "name": "Motion",
            "iconURL": "/icons/Motion.PNG"
        },
        {
            "_id": "603e53ea6a94d1071cb7b46c",
            "name": "Appearing",
            "iconURL": "/icons/Motion_Appearing.PNG"
        },
        ...
    ],
    "_id": "603e54ba4a5f860734fa3b45",
    "title": "Stepping feet« illusion 1 – strong",
    "gifFileName": "/gifs/“Stepping feet” Motion Illusion.gif",
    "refURL": "https://michaelbach.de/ot/mot-feetLin/index.html",
    "update_at": "2021-03-02T15:07:38.444Z",
    "__v": 0
}
```
#### Description
Get a certain illusion by UID

---
### GET /tags/\[elements|effects\]?populate=\[true|false\]
#### Request
```
No need to send anything
```
#### Response
```javascript=
// Unpopulated
[
    {
        "_id": "603e53ea6a94d1071cb7b467",
        "name": "Translation",
        "iconURL": "/icons/Motion_Variation_Translation.PNG"
    },
    {
        "_id": "603e53ea6a94d1071cb7b474",
        "name": "Brightness",
        "iconURL": "/icons/Color_Variation_Brightness.PNG"
    },
    ...
]
// Populated
[
  {
    "subeffects": [
        {
            "subeffects": [
                {
                    "subeffects": [],
                    "_id": "603e53ea6a94d1071cb7b46d",
                    "level": 2,
                    "name": "Translation",
                    "iconURL": "/icons/Motion_Appearing_Translation.PNG"
                },
                ...
            ],
            "_id": "603e53ea6a94d1071cb7b46c",
            "level": 1,
            "name": "Appearing",
            "iconURL": "/icons/Motion_Appearing.PNG"
        }
    ],
    "_id": "603e53ea6a94d1071cb7b465",
    "level": 0,
    "name": "Motion",
    "iconURL": "/icons/Motion.PNG"
  }
]
```
#### Description
Get all tags (elements OR effects).
If you want to retrieve tag hierarchy as well, set `populate=true`.

---

### POST '/illusions/' (Work In Progress, DO NOT USE)
#### Request
#### Request
```
Header:
    Content-Type: application/json
Body:
    {
      "title": "Moire Patterns",
      "elements": [
        "602d0e92771e4402dd6854d2&602d0e92771e4402dd6854d8&602d0e92771e4402dd6854ea&602d0e92771e4402dd6854f3&602d0e92771e4402dd685501",
        "602d0e92771e4402dd6854fe&602d0e92771e4402dd6854db&602d0e92771e4402dd6854f4&602d0e92771e4402dd685505&602d0e92771e4402dd685508"
      ],
      "effects": [
        "602d0e92771e4402dd68551f",
        "602d0e92771e4402dd68552c",
        "602d0e92771e4402dd685534"
      ],
      "gifFileName": "FILE NAME OF GIF",
      "refURL": "URL TO THE REFERENCE",
      "summary": "MARKDOWN CONTENT GOES HERE, BUT YOU NEED TO REPLACE ALL LINEBREAKS WITH /n"
    }
```
#### Response
```
DONE
```
#### Description
Register a new illusion into database. 
**Notice: tag id with the same abstract level should be seperate with an '&'** 

---

### POST /illusions/search/\[elements|effects\]
```
Header:
    Content-Type: application/json
Body:
    {
      "tags": [
        "602d0e92771e4402dd685536&602d0e92771e4402dd68551f",
        "602d0e92771e4402dd68553f&602d0e92771e4402dd685520",
        "602d0e92771e4402dd685528&602d0e92771e4402dd685529"
      ]
    }
```
#### Response
```javascript=
[
    "603100add77a41032a433b46"
]
```
#### Description
Find illusions with tags.
**Notice: tag id with the same abstract level should be seperate with an '&'** 
